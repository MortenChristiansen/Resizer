using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resizer.Views
{
    public interface IResizingView : IView
    {
        int Progress { get; set; }
        int Dimension { get; set; }
        int Quality { get; set; }
        string Url { get; set; }
        string Title { get; set; }
        ResizingApproach ResizingApproach { get; set; }
        bool ResizingActionEnabled { get; set; }
        bool CancelResizingActionEnabled { get; set; }
        bool PreserveMetaData { get; set; }

        event EventHandler ResizeImages;
        event EventHandler CancelResizing;
        event EventHandler CloseWindow;
        event EventHandler SetCurrentDirectory;
    }
}
