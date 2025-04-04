using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Helpers;

namespace WpfApp1.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string _upperString ="";
        public string UpperString
        {
            get { return _upperString; }
            private set 
            {
                SetProperty(ref this._upperString, value);
            }
        }

        private string _inputString = "";
        public string InputString
        {
            get { return _inputString; }
            set
            {
                if (SetProperty(ref _inputString, value))
                {
                    this.UpperString = this._inputString.ToUpper();
                    System.Diagnostics.Debug.WriteLine("UpperString=" + this.UpperString);
                    //コマンドの実行可能判別結果に影響を与えているので変更通知を行う
                    ClearCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _fooString = "";
        public string FooString
        {
            get { return _fooString; }
            private set
            {
                if (_fooString != value)
                {
                    _fooString = value;
                }
            }
        }

        private string _barString = "";
        public string BarString
        {
            get { return _barString; }
            set
            {
                if (_barString != value)
                {
                    _barString = value;
                    FooString = _barString.ToUpper();
                    System.Diagnostics.Debug.WriteLine("FooString=" + FooString);
                }
            }
        }

        private DelegateCommand _clearCommand;
        public DelegateCommand ClearCommand
        {
            get 
            {
                return _clearCommand ?? (_clearCommand = new DelegateCommand(
                    _ => InputString = "",
                    _ => !string.IsNullOrEmpty(InputString)));
            }
        }
    }
}
