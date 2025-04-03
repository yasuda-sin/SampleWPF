using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Helpers;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    internal class TaskViewModel : ViewModelBase
    {
        private readonly TaskService _taskService;
        private ObservableCollection<TaskItem> _taskItems = new ObservableCollection<TaskItem>();

        public ObservableCollection<TaskItem> TaskItems
        {
            get { return _taskItems; }
            private set { SetProperty(ref _taskItems, value); }
        }

        public DelegateCommand LoadTasksCommand { get; }
        public TaskViewModel()
        {
            _taskService = new TaskService();
            LoadTasksCommand = new DelegateCommand(async () => await LoadTaskItems());
            LoadTaskItems();
        }

        private async Task LoadTaskItems()
        {
            var taskItems = await _taskService.GetAllTaskItemsAsync();
            TaskItems = new ObservableCollection<TaskItem>(taskItems);
        }

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new DelegateCommand(
                    _ =>
                    {
                        System.Diagnostics.Debug.WriteLine("追加しました");
                    }));
            }
        }

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new DelegateCommand(
                    t =>
                    {
                        DeleteCommand.RaiseCanExecuteChanged();
                    }));
            }
        }

        private DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand
        {
            get { return _updateCommand; }
        }
    }
}
