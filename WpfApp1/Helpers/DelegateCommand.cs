using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Helpers
{
    internal class DelegateCommand : ICommand
    {
        private Action<object> _execute;
        //入力にparameter, 戻り値にboolのメソッドのデリゲート
        private Func<object, bool> _canExecute;

        public DelegateCommand(Action execute) : this(parameter => execute(), null)
        {
        }
        public DelegateCommand(Action<object> execute) : this(execute, null) 
        {
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// コマンドが実行可能かどうかを判別する処理を行う
        /// falseを返した場合、データバインディングした先のコントロールが無効状態となる
        /// </summary>
        /// <param name="parameter">判別処理に関するパラメータを指定</param>
        /// <returns>実行可能な場合はtrueを返す</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        //CanExecute()により実行可能かどうかの状態が変更されたときに発生しUIへ通知する
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// CanExecuteChangedイベントを発行
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var h = CanExecuteChanged;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }
        
        /// <summary>
        /// コマンドが実行されたときの処理を行う
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute(parameter);
            }
        }
    }
}
