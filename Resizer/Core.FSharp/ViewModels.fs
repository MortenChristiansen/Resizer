namespace Core.FSharp.ViewModels

open Core.FSharp.Models
open ViewModelSupport
open System.IO
open System.Windows
open System.Drawing
open Imaging
open ViewModelSupport
open ImpromptuInterface.FSharp
open System.Threading
open AsyncExtensions

type MainViewModelF() as this = 
    inherit ViewModelBase()
    
    let mutable cancellationSource = new CancellationTokenSource()

    do
        this.Set("Url", "")
        this.Set("ResizingApproach", ResizingApproach.Max)
        this.Set("Progress", 0.0)
        this.Set("Dimension", 1200)
        this.Set("JpgQuality", 90)
        this.Set("PreserveMetadata", true)
        this.Set("ResizingInProgress", false)

    [<ViewModelBase.DependsUpon("ResizingInProgress")>]
    member this.CanExecute_Resize() =
        not this?ResizingInProgress

    member this.Execute_Resize() =
        let dir = new DirectoryInfo(this?Url)
        if not dir.Exists then
            MessageBox.Show("No such directory exists!") |> ignore
        else
            let outputDir = new DirectoryInfo(Path.Combine(this?Url, "resized/"))
            if not outputDir.Exists then outputDir.Create()
            
            cancellationSource.Cancel()
            cancellationSource <- new CancellationTokenSource()

            this?ResizingInProgress <- true
            this?Progress <- 0.0
            
            let files = dir.GetFiles() |> Seq.filter (fun f -> f.Extension.ToLower() = ".jpg") |> Seq.toArray 
            let event = SynchronizationContext.CreateProgressEvent (fun p -> this?Progress <- p) files.Length

            let settings = { Dimension = this?Dimension; JpgQuality = this?JpgQuality; PreserveMetadata = this?PreserveMetadata; ResizingApproach = this?ResizingApproach }
            
            let processFileIfNotCancelled file =
                if not cancellationSource.Token.IsCancellationRequested then 
                    Imaging.processFile settings outputDir file
                    event.ReportProgress()

            let t = async {
                files |> Array.Parallel.iter processFileIfNotCancelled 
                do! Async.SwitchToContext event.Context
                this?ResizingInProgress <- false
            }
            Async.Start(t, cancellationSource.Token)
            ()
            
    [<ViewModelBase.DependsUpon("ResizingInProgress")>]
    member this.CanExecute_SetCurrentPath() =
        not this?ResizingInProgress

    member this.Execute_SetCurrentPath() =
        this?Url <- Directory.GetCurrentDirectory()

    [<ViewModelBase.DependsUpon("ResizingInProgress")>]
    member this.CanExecute_Cancel() =
        not (not this?ResizingInProgress)

    member this.Execute_Cancel() =
        cancellationSource.Cancel()
        this?ResizingInProgress <- false