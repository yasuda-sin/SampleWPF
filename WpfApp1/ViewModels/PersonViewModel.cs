using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Helpers;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    internal class PersonViewModel : ViewModelBase
    {
        /// <summary>
        /// ObservableCollectionによってUIへPeopleの子要素person が変更されたことを通知する
        /// RaisePropertyChanged("People")では、Peopleプロパティの変更通知を行うが、子要素追加削除はPeopleが変更されるわけではない
        /// INotifyCollectionChangedインターフェースによるCollectionChangedイベントを発行する必要があり、その実装がObservableCollection<T>.
        /// </summary>
        private ObservableCollection<Person> _people = new ObservableCollection<Person>();
        public ObservableCollection<Person> People
        {
            get { return _people; }
            private set { SetProperty(ref _people, value); }
        }

        private int _count;
        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new DelegateCommand(
                    _ =>
                    {
                        _count++;
                        var person = new Person()
                        {
                            FamilyName = "田中",
                            FirstName = _count + "太郎",
                            Age = _count
                        };
                        People.Add(person);
                        DeleteCommand.RaiseCanExecuteChanged();
                        System.Diagnostics.Debug.WriteLine(person.FullName + "を追加しました");
                    }));
            }
        }

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new DelegateCommand(
                    p =>
                    {
                        People.Remove(p as Person);
                        DeleteCommand.RaiseCanExecuteChanged();
                    }));
            }
        }
    }
}
