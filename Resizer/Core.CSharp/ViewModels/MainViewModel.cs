using Core.CSharp.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Core.CSharp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private BackgroundWorker _backgroundWorker;

        #region Properties

        private bool _resizingInProgress;
        private bool ResizingInProgress
        {
            get { return _resizingInProgress; }
            set
            {
                _resizingInProgress = value;
                _resizeCommand.RaiseCanExecuteChanged();
                _cancelCommand.RaiseCanExecuteChanged();
                _setCurrentPathCommand.RaiseCanExecuteChanged();
            }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { Set(() => Url, ref _url, value); }
        }

        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set { Set(() => Progress, ref _progress, value); }
        }

        private int _dimension;
        public int Dimension
        {
            get { return _dimension; }
            set { Set(() => Dimension, ref _dimension, value); }
        }

        private ResizingApproach _resizingApproach;
        public ResizingApproach ResizingApproach
        {
            get { return _resizingApproach; }
            set { Set(() => ResizingApproach, ref _resizingApproach, value); }
        }

        private int _jpgQuality;
        public int JpgQuality
        {
            get { return _jpgQuality; }
            set { Set(() => JpgQuality, ref _jpgQuality, value); }
        }

        private bool _preserveMetadata;
        public bool PreserveMetadata
        {
            get { return _preserveMetadata; }
            set { Set(() => PreserveMetadata, ref _preserveMetadata, value); }
        }

        private RelayCommand _setCurrentPathCommand;
        public ICommand SetCurrentPathCommand
        {
            get { return _setCurrentPathCommand; }
        }

        private RelayCommand _resizeCommand;
        public ICommand ResizeCommand
        {
            get { return _resizeCommand; }
        }

        private RelayCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }

        #endregion

        public MainViewModel()
        {
            LoadSettings();
            InitializeCommands();
            ConfigureBackgroundWorker();
        }

        private void LoadSettings()
        {
            var settings = Core.CSharp.CSharpSettings.Default;
            PreserveMetadata = settings.SaveMetadata;
            JpgQuality = settings.EncodingQuality;
            Dimension = settings.MaxDimension;
            ResizingApproach = (ResizingApproach)settings.ResizingApproach;
        }

        private void SaveSettings()
        {
            var settings = Core.CSharp.CSharpSettings.Default;
            settings.SaveMetadata = PreserveMetadata;
            settings.EncodingQuality = JpgQuality;
            settings.MaxDimension = Dimension;
            settings.ResizingApproach = (int)ResizingApproach;
            settings.Save();
        }

        private void InitializeCommands()
        {
            _setCurrentPathCommand = new RelayCommand(SetCurrentPath, CanSetCurrentPath);
            _resizeCommand = new RelayCommand(Resize, CanResize);
            _cancelCommand = new RelayCommand(Cancel, CanCancel);
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

        private void SetCurrentPath()
        {
            if (!CanSetCurrentPath()) return;

            Url = Directory.GetCurrentDirectory();
            SaveSettings();
        }

        private bool CanSetCurrentPath()
        {
            return !_resizingInProgress;
        }

        private void Resize()
        {
            if (!CanResize()) return;

            SaveSettings();
            Progress = 0;
            _backgroundWorker.RunWorkerAsync();
        }

        private bool CanResize()
        {
            return !_resizingInProgress;
        }

        private void Cancel()
        {
            if (!CanCancel()) return;

            _backgroundWorker.CancelAsync();
        }

        private bool CanCancel()
        {
            return _resizingInProgress;
        }

        private void OnCompleted(object sender, RunWorkerCompletedEventArgs completedArgs)
        {
            ResizingInProgress = false;
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs progressArgs)
        {
            Progress = progressArgs.ProgressPercentage;
        }

        private void BeginResizingJob(object sender, DoWorkEventArgs e)
        {
            var dir = new DirectoryInfo(Url);
            if (!dir.Exists)
            {
                MessageBox.Show("No such directory exists!");
                return;
            }

            ResizingInProgress = true;

            var resizedDir = new DirectoryInfo(Path.Combine(Url, "resized/"));
            if (!resizedDir.Exists) resizedDir.Create();

            var jpegs = dir.GetFiles().Where(file => file.Extension.ToLower() == ".jpg").ToList();

            int current = 0;
            foreach (var imageFile in jpegs)
            {
                if (_backgroundWorker.CancellationPending) break;

                var original = new Bitmap(imageFile.FullName);
                Image resized = ImageHelper.ResizeImage(original, Dimension, ResizingApproach);
                if (PreserveMetadata) ImageHelper.CopyImageProperties(original, resized);
                string savename = resizedDir.FullName + imageFile.Name;
                ImageHelper.SaveJpg(resized, savename, JpgQuality);
                current++;

                _backgroundWorker.ReportProgress((int)((current / (float)jpegs.Count) * 100));
            }

            SaveSettings();
        }
    }
}
