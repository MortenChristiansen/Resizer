module ``Testing view models: ``

open Xunit
open FsUnit.Xunit
open Core.FSharp.Models
open Core.FSharp.ViewModels
open ImpromptuInterface.FSharp
open System.IO

[<Trait("View models","MainViewModelF")>]
type ``MainViewModelF ``() =

    let sut = new MainViewModelF()

    [<Fact>] 
    let ``Is constructed with default property values`` () =
        sut?Url |> should equal ""
        sut?ResizingApproach |> should equal ResizingApproach.Max
        sut?Progress |> should equal 0.0
        sut?Dimension |> should equal 1200
        sut?JpgQuality |> should equal 90
        sut?PreserveMetadata |> should be True
        sut?ResizingInProgress |> should be False

    [<Fact>]
    let ``When setting the current path, the Url property is set to the current directory`` () =
        sut.Execute_SetCurrentPath()
        sut?Url |> should equal (Directory.GetCurrentDirectory())

    [<Fact>]
    let ``When resizing is not in progress, you cannot cancel the resize operation`` () =
        sut?CanExecute_Cancel() |> should be False

    [<Fact>]
    let ``When resizing is in progress, you can cancel the resize operation`` () =
        sut?ResizingInProgress <- true
        sut?CanExecute_Cancel() |> should be True

    [<Fact>]
    let ``When resizing is not in progress, you cannot set current path`` () =
        sut?CanExecute_SetCurrentPath() |> should be False

    [<Fact>]
    let ``When resizing is in progress, you can set current path`` () =
        sut?ResizingInProgress <- true
        sut?CanExecute_SetCurrentPath() |> should be True