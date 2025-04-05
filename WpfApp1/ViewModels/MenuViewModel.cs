using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Helpers;
using WpfApp1.Views.Behaviors;

namespace WpfApp1.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        #region ファイルを開く
        private DelegateCommand _openFileCommand;
        public DelegateCommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ?? (_openFileCommand = new DelegateCommand(
                    _ =>
                    {
                        DialogCallback = OnDialogCallback;
                    }));
            }
        }

        private Action<bool, string> _dialogCallback;
        public Action<bool, string> DialogCallback
        {
            get { return _dialogCallback; }
            private set {  SetProperty(ref _dialogCallback, value); }
        }

        /// <summary>
        /// Dialogに対するCallback処理を行う
        /// </summary>
        /// <param name="isOk">ダイアログの結果を指定する</param>
        /// <param name="filePath">ファイルのフルパスを指定する</param>
        private void OnDialogCallback(bool isOk, string filePath)
        {
            DialogCallback = null;  //コールバック時にプロパティを初期化
            System.Diagnostics.Debug.WriteLine($"isOk :{isOk}, filePath :{filePath}");
        }
        #endregion

        #region アプリの終了
        public Func<bool> ClosingCallback
        {
            get { return OnExit; }
        }

        private DelegateCommand _exitCommand;
        public DelegateCommand ExitCommand
        {
            get 
            {
                return _exitCommand ?? (_exitCommand = new DelegateCommand(
                    _ =>
                    {
                        OnExit();
                    }));
            }
        }

        private int _counter = 0;
        private bool OnExit()
        {
            if (_counter < 3)
            {
                _counter++;
                return false;
            }
            App.Current.Shutdown();
            return true;
        }
        #endregion

        #region version情報を表示する
        private VersionViewModel _versionViewModel = new VersionViewModel();
        public VersionViewModel VersionViewModel
        {
            get { return _versionViewModel; }
        }
        private DelegateCommand _versionDialogCommand;
        public DelegateCommand VersionDialogCommand
        {
            get
            {
                return _versionDialogCommand ?? (_versionDialogCommand = new DelegateCommand(
                    _ =>
                    {
                        VersionDialogCallback = OnVersionDialog;
                    }));
            }
        }
        private Action<bool> _versionDialogCallback;
        public Action<bool> VersionDialogCallback
        {
            get { return _versionDialogCallback; }
            private set { SetProperty(ref _versionDialogCallback, value); }
        }
        private void OnVersionDialog(bool result)
        {
            VersionDialogCallback = null;
            System.Diagnostics.Debug.WriteLine(result);
        }
        #endregion

        #region 現在時刻を取得する
        private System.Timers.Timer _timer;
        private DateTime _currentTime;

        public DateTime CurrentTime
        {
            get
            {
                if (_timer is null)
                {
                    _currentTime = DateTime.Now;
                    _timer = new System.Timers.Timer(1000);
                    _timer.Elapsed += (_, _) =>
                    {
                        CurrentTime = DateTime.Now;
                    };
                    _timer.Start();
                }
                return _currentTime;
            }
            private set { SetProperty(ref _currentTime, value); }
        }
        #endregion


        #region タスク管理画面
        private TaskViewModel _taskViewModel = new TaskViewModel();
        public TaskViewModel TaskViewModel
        {
            get { return _taskViewModel; }
        }
        private DelegateCommand _taskDialogCommand;
        public DelegateCommand TaskDialogCommand
        {
            get
            {
                return _taskDialogCommand ?? (_taskDialogCommand = new DelegateCommand(
                    _ =>
                    {
                        TaskDialogCallback = OnTaskDialog;
                    }));
            }
        }
        private Action<bool> _taskDialogCallback;
        public Action<bool> TaskDialogCallback
        {
            get { return _taskDialogCallback; }
            private set { SetProperty(ref _taskDialogCallback, value); }
        }
        private void OnTaskDialog(bool result)
        {
            TaskDialogCallback = null;
            System.Diagnostics.Debug.WriteLine(result);
        }
        #endregion
    }
}
