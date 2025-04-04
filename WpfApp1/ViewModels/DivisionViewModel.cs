using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Helpers;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    class DivisionViewModel : ViewModelBase
    {
        private Calculator _calc;
        public DivisionViewModel()
        {
            _calc = new Calculator();
        }

        private string _lhs;
        public string LHS
        {
            get { return _lhs; }
            set
            {
                if (SetProperty(ref _lhs, value))
                {
                    DivCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _rhs;
        public string RHS
        {
            get { return _rhs; }
            set 
            {
                if (SetProperty(ref _rhs, value))
                {
                    DivCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }

        private DelegateCommand _divCommand;

        public DelegateCommand DivCommand
        {
            get
            {
                return _divCommand ?? (_divCommand = new DelegateCommand(
                    _ => OnDivision(),
                    _ =>
                    {
                        if (!double.TryParse(LHS, out double _))
                            return false;
                        if (!double.TryParse(RHS, out double _))
                            return false;
                        return true;
                    }));
            }
        }
        private void OnDivision()
        {
            _calc.Lhs = double.Parse(LHS);
            _calc.Rhs = double.Parse(RHS);
            _calc.CalcExec();
            Result = _calc.Result.ToString();
        }
    }
}
