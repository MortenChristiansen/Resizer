module AsyncExtensions

open System.Threading

type EventContext = {
    Event:Event<unit>;
    Context:SynchronizationContext;
    ReportProgress:unit -> unit
}

type SynchronizationContext with
    /// A standard helper extension method to raise an event on the GUI thread
    member syncContext.RaiseEvent (event: Event<_>) args =
        syncContext.Post((fun _ -> event.Trigger args),state=null)
 
    /// A standard helper extension method to capture the current synchronization context.
    /// If none is present, use a context that executes work in the thread pool.
    static member CaptureCurrent () =
        match SynchronizationContext.Current with
        | null -> new SynchronizationContext()
        | ctxt -> ctxt

    static member CreateProgressEvent onProgress (items:int) =
        let event = new Event<unit>()
        event.Publish |> Observable.scan (fun i _ -> i + 1) 0 |> Observable.subscribe (fun i -> onProgress ((float (i * 100)) / (float items))) |> ignore
        let context = SynchronizationContext.CaptureCurrent()
        let triggerProgress() =
            context.RaiseEvent event ()
        { Event=event; Context=context; ReportProgress=triggerProgress }
