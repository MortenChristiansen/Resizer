using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resizer.Views;
using Resizer.Properties;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Resizer.Presenters
{
    public class ResizingPresenter
    {
        private IResizingView _view;
        private BackgroundWorker _backgroundWorker;

        public ResizingPresenter(IResizingView view)
        {
            _view = view;
            ConfigureBackgroundWorker();
            RegisterEventHandlers();
            LoadViewProperties();
        }

        private void RegisterEventHandlers()
        {
            _view.ResizeImages += OnStart;
            _view.SetCurrentDirectory += OnSetCurrentDirectory;
            _view.CloseWindow += OnCloseWindow;
            _view.CancelResizing += OnCancelResizing;
        }

        private void LoadViewProperties()
        {
            Settings settings = Resizer.Properties.Settings.Default;
            _view.PreserveMetaData = settings.SaveMetaData;
            _view.Quality = settings.EncodingQuality;
            _view.Dimension = settings.MaxDimension;
            _view.ResizingApproach = (ResizingApproach)settings.ResizingApproach;
            _view.Title = string.Format("Resizer v{0}.{1}", Application.ProductVersion.Split('.')[0], Application.ProductVersion.Split('.')[1]);
        }

        private void SaveSettings()
        {
            Settings settings = Resizer.Properties.Settings.Default;
            settings.SaveMetaData = _view.PreserveMetaData;
            settings.EncodingQuality = _view.Quality;
            settings.MaxDimension = _view.Dimension;
            settings.ResizingApproach = (int)_view.ResizingApproach;
            settings.Save();
        }

        private static Image ResizeImage(Image originalBitmap, int maxDimension, ResizingApproach resizingApproach)
        {
            float aspectX = maxDimension / (float)originalBitmap.Width;
            float aspectY = maxDimension / (float)originalBitmap.Height;

            float actualAspect = 0;
            if (resizingApproach == ResizingApproach.Max) actualAspect = Math.Min(aspectX, aspectY);
            else if (resizingApproach == ResizingApproach.Min) actualAspect = Math.Max(aspectX, aspectY);
            else if (resizingApproach == ResizingApproach.Horizontal) actualAspect = aspectX;
            else if (resizingApproach == ResizingApproach.Vertical) actualAspect = aspectY;

            int sourceWidth = (int)(originalBitmap.Width * actualAspect);
            int sourceHeight = (int)(originalBitmap.Height * actualAspect);

            var resized = new Bitmap(sourceWidth, sourceHeight);

            Graphics g = Graphics.FromImage(resized);
            g.DrawImage(originalBitmap, new Rectangle(0, 0, resized.Width, resized.Height));
            return resized;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(codecInfo => codecInfo.MimeType == mimeType);
        }

        private void SaveJpg(Image image, string fileName, long quality)
        {
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ImageCodecInfo codecInfo = GetEncoderInfo("image/jpeg");
            image.Save(fileName, codecInfo, parameters);
        }

        private void SavePng(Image image, string fileName)
        {
            image.Save(fileName, ImageFormat.Png);
        }

        private void CopyImageProperties(Image source, Image recepient)
        {
            foreach (PropertyItem prop in source.PropertyItems)
            {
                recepient.SetPropertyItem(prop);
            }
        }

        private void ConfigureBackgroundWorker()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.DoWork += BeginResizingJob;
            _backgroundWorker.RunWorkerCompleted += OnCompleted;
            _backgroundWorker.ProgressChanged += OnProgressChanged;
        }

        void OnCloseWindow(object sender, EventArgs e)
        {
            SaveSettings();
        }

        void OnSetCurrentDirectory(object sender, EventArgs e)
        {
            _view.Url = Application.StartupPath;
        }

        void OnCancelResizing(object sender, EventArgs e)
        {
            _backgroundWorker.CancelAsync();
        }

        void OnStart(object sender, EventArgs e)
        {
            _view.Progress = 0;
            _backgroundWorker.RunWorkerAsync();
            _view.CancelResizingActionEnabled = true;
            _view.ResizingActionEnabled = false;
        }

        void OnCompleted(object sender, RunWorkerCompletedEventArgs completedArgs)
        {
            _view.CancelResizingActionEnabled = false;
            _view.ResizingActionEnabled = true;
        }

        void OnProgressChanged(object sender, ProgressChangedEventArgs progressArgs)
        {
            _view.Progress = progressArgs.ProgressPercentage;
        }

        private void BeginResizingJob(object sender, DoWorkEventArgs e)
        {
            var dir = new DirectoryInfo(_view.Url);
            if (!dir.Exists)
            {
                MessageBox.Show("No such directory exists!");
                return;
            }

            var resizedDir = new DirectoryInfo(Path.Combine(_view.Url, "resized/"));
            if (!resizedDir.Exists) resizedDir.Create();

            var jpegs = GetFilesWithExtension(dir, "jpg");
            jpegs.AddRange(GetFilesWithExtension(dir, "jpeg"));
            var pngs = GetFilesWithExtension(dir, "png");
            var images = jpegs.Concat(pngs).ToList();

            int current = 0;
            foreach (var imageFile in images)
            {
                if (_backgroundWorker.CancellationPending) break;

                var original = new Bitmap(imageFile.FullName);
                Image resized = ResizeImage(original, _view.Dimension, _view.ResizingApproach);
                if (_view.PreserveMetaData) CopyImageProperties(original, resized);
                string savename = resizedDir.FullName + imageFile.Name;
                if (imageFile.Extension.ToLower() == ".jpg")
                    SaveJpg(resized, savename, _view.Quality);
                else
                    SavePng(resized, savename);
                current++;

                _backgroundWorker.ReportProgress((int)((current / (float)images.Count) * 100));
            }

            SaveSettings();
        }

        private List<FileInfo> GetFilesWithExtension(DirectoryInfo dir, string extension)
        {
            return dir.GetFiles().Where(file => file.Extension.ToLower() == "." + extension).ToList();
        }
    }
}
