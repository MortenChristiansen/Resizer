module ``Testing utilities: ``

open Xunit
open FsUnit.Xunit
open Core.FSharp.Models
open System.IO
open System.Drawing

[<Trait("Utilities","Imaging")>]
type ``class Imaging ``() =

    [<Fact>]
    let ``When resizing an image by the maximum dimension, the size is set accordingly`` () =
        let original = new Bitmap(200, 100)
        let result = Imaging.resizeImage original 50.0 ResizingApproach.Max
        result.Width |> should equal 50
        result.Height |> should equal 25

    [<Fact>]
    let ``When resizing an image by the minimum dimension, the size is set accordingly`` () =
        let original = new Bitmap(200, 100)
        let result = Imaging.resizeImage original 50.0 ResizingApproach.Min
        result.Width |> should equal 100
        result.Height |> should equal 50

    [<Fact>]
    let ``When resizing an image by the horizontal dimension, the size is set accordingly`` () =
        let original = new Bitmap(200, 100)
        let result = Imaging.resizeImage original 50.0 ResizingApproach.Horizontal
        result.Width |> should equal 50
        result.Height |> should equal 25

    [<Fact>]
    let ``When resizing an image by the vertical dimension, the size is set accordingly`` () =
        let original = new Bitmap(200, 100)
        let result = Imaging.resizeImage original 50.0 ResizingApproach.Vertical
        result.Width |> should equal 100
        result.Height |> should equal 50

    [<Fact>]
    let ``When retrieving decoder info for a correct MIME type, a value is returned`` () =
        Imaging.getEncoderInfo "image/jpeg" |> should not' (be Null)