namespace Core.FSharp.Converters

open System
open System.Windows.Data

type EnumToBoolConverter() =
    interface IValueConverter with
        member this.Convert(value, targetType, parameter, culture) =
            value.ToString() = parameter.ToString() :> obj

        member this.ConvertBack(value, targetType, parameter, culture) =
            Enum.Parse(targetType, parameter.ToString())