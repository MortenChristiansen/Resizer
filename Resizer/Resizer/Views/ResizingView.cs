using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Resizer.Properties;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel;
using Resizer.Views;
using Resizer.Presenters;

namespace Resizer
{
    public partial class ResizeForm : Form, IResizingView
    {
        public ResizeForm()
        {
            InitializeComponent();

            SetToolTip(setCurrentDirButton, "Use current directory");
            this.FormClosing += new FormClosingEventHandler(ResizeForm_FormClosing);
        }

        #region UI Helper Methods

        private void SetToolTip(Control control, string message)
        {
            var tip = new ToolTip();

            tip.SetToolTip(control, message);
        }

        private ResizingApproach GetResizingApproach()
        {
            if (maxRadio.Checked) return ResizingApproach.Max;
            if (minRadio.Checked) return ResizingApproach.Min;
            if (horizontalRadio.Checked) return ResizingApproach.Horizontal;
            if (verticalRadio.Checked) return ResizingApproach.Vertical;
            throw new ArgumentException("Invalid resizing approach.");
        }

        private void SetResizingApproach(ResizingApproach approach)
        {
            maxRadio.Checked = approach == ResizingApproach.Max;
            minRadio.Checked = approach == ResizingApproach.Min;
            horizontalRadio.Checked = approach == ResizingApproach.Horizontal;
            verticalRadio.Checked = approach == ResizingApproach.Vertical;
        }

        #endregion

        #region User Interactions

        void ResizeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseWindow != null) CloseWindow(this, new EventArgs());
        }

        private void resizeButton_Click(object sender, EventArgs e)
        {
            if (ResizeImages != null) ResizeImages(this, new EventArgs());
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (CancelResizing != null) CancelResizing(this, new EventArgs());
        }

        private void setCurrentDirButton_Click(object sender, EventArgs e)
        {
            if (SetCurrentDirectory != null) SetCurrentDirectory(this, new EventArgs());
        }

        #endregion

        #region IResizingView Members

        public int Progress
        {
            get
            {
                return progressBar.Value;
            }
            set
            {
                progressBar.Value = value;
            }
        }

        public int Dimension
        {
            get
            {
                int val = 0;
                int.TryParse(dimensionBox.Text, out val);
                return val;
            }
            set
            {
                dimensionBox.Text = value.ToString();
            }
        }

        public int Quality
        {
            get
            {
                return (int)qualityScale.Value;
            }
            set
            {
                qualityScale.Value = value;
            }
        }

        public string Url
        {
            get
            {
                return urlBox.Text;
            }
            set
            {
                urlBox.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        public ResizingApproach ResizingApproach
        {
            get
            {
                return GetResizingApproach();
            }
            set
            {
                SetResizingApproach(value);
            }
        }

        public bool ResizingActionEnabled
        {
            get
            {
                return resizeButton.Enabled;
            }
            set
            {
                resizeButton.Enabled = value;
            }
        }

        public bool CancelResizingActionEnabled
        {
            get
            {
                return cancelButton.Enabled;
            }
            set
            {
                cancelButton.Enabled = value;
            }
        }

        public bool PreserveMetaData
        {
            get
            {
                return metaDataCheck.Checked;
            }
            set
            {
                metaDataCheck.Checked = value;
            }
        }

        public event EventHandler ResizeImages;
        public event EventHandler CancelResizing;
        public event EventHandler CloseWindow;
        public event EventHandler SetCurrentDirectory;

        #endregion

        #region IView Members

        private ResizingPresenter _presenter;
        public void Initialize()
        {
            _presenter = new ResizingPresenter(this);
        }

        #endregion
    }
}
