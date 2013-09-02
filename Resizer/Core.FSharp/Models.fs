namespace Core.FSharp.Models

type ResizingApproach =
    | Max = 0
    | Min = 1
    | Horizontal = 2
    | Vertical = 3

type ResizingSettings = {
    ResizingApproach: ResizingApproach
    Dimension: int
    JpgQuality: int
    PreserveMetadata: bool
}