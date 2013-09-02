module Imaging

open System.Drawing
open System.Drawing.Imaging
open System
open Core.FSharp.Models
open System.IO

let resizeImage (originalBitmap:Bitmap) maxDimension resizingApproach =

    let getActualAspect approach (originalBitmap:Bitmap) =
        let aspectX = maxDimension / (float originalBitmap.Width)
        let aspectY = maxDimension / (float originalBitmap.Height)
        match approach with
        | ResizingApproach.Max -> Math.Min(aspectX, aspectY)
        | ResizingApproach.Min -> Math.Max(aspectX, aspectY)
        | ResizingApproach.Horizontal -> aspectX
        | ResizingApproach.Vertical -> aspectY
        | _ -> 0.0

    let createNewBitmap original aspect =
        let sourceWidth = (float originalBitmap.Width) * aspect
        let sourceHeight = (float originalBitmap.Height) * aspect
        new Bitmap(int sourceWidth, int sourceHeight)

    let drawResizedImage original resized =
        let g = Graphics.FromImage(resized)
        g.DrawImage(original, new Rectangle(0, 0, resized.Width, resized.Height))

    let actualAspect = getActualAspect resizingApproach originalBitmap
    let resized = createNewBitmap originalBitmap actualAspect
    drawResizedImage originalBitmap resized
    resized

let getEncoderInfo mimeType =
    let matchCodec mime (codec:ImageCodecInfo) =
        if codec.MimeType = mime then Some(codec) else None

    ImageCodecInfo.GetImageEncoders() |> Seq.pick (matchCodec mimeType)

let saveJpg (image:Image) (filename:string) (quality:int64) =
    let parameters = new EncoderParameters(1)
    parameters.Param.[0] <-  new EncoderParameter(Encoder.Quality, quality)
    let codecInfo = getEncoderInfo "image/jpeg"
    image.Save(filename, codecInfo, parameters)

let copyImageProperties (source:Image) (recipient:Image) =
    source.PropertyItems |> Seq.iter (fun p -> recipient.SetPropertyItem(p))

let processFile (settings:ResizingSettings) (resizedDir:DirectoryInfo) (imageFile:FileInfo) =
    let test = settings.PreserveMetadata
    let original = new Bitmap(imageFile.FullName)
    let resized = resizeImage original (float settings.Dimension) settings.ResizingApproach
    if settings.PreserveMetadata then copyImageProperties original resized
    let savename = resizedDir.FullName + imageFile.Name
    saveJpg resized savename (int64 settings.JpgQuality)
    original.Dispose()
    resized.Dispose()