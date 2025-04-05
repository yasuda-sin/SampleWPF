using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Helpers;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    internal class TaskViewModel : ViewModelBase
    {
        private string _taskName;
        public string TaskName 
        {
            get => _taskName;
            set  => SetProperty(ref _taskName, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public byte Status { get; private set; } = (byte)Models.TaskStatus.NotStarted;

        public DateTime CreatedDateTime { get; private set; } = DateTime.Now;

        private DateTime _deadline = DateTime.Now;
        public DateTime Deadline 
        {
            get => _deadline;
            set => SetProperty(ref _deadline, value);
        }

        private readonly TaskService _taskService;
        private ObservableCollection<TaskItem> _taskItems = new ObservableCollection<TaskItem>();
        public ObservableCollection<TaskItem> TaskItems
        {
            get  => _taskItems;
            set => SetProperty(ref _taskItems, value);
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
                    async(t) =>
                    {
                        if (string.IsNullOrEmpty(TaskName) || string.IsNullOrEmpty(Description))
                        {
                            return;
                        }

                        var newTaskItem = new TaskItem
                        {
                            TaskName = TaskName,
                            Description = Description,
                            CreatedDateTime = CreatedDateTime,
                            Deadline = Deadline
                        };
                        var (isSuccess, newId) = await _taskService.AddTaskItemAsync(newTaskItem);
                        if (isSuccess)
                        {
                            newTaskItem.TaskId = newId;
                            newTaskItem.UpdatedDateTime = DateTime.MinValue;
                            _taskItems.Add(newTaskItem);
                            DeleteCommand.RaiseCanExecuteChanged();
                            
                            TaskName = string.Empty;
                            Description = string.Empty;
                            Deadline = DateTime.Now;
                            MessageBox.Show("追加しました", "通知", MessageBoxButton.OK);
                        }
                    }));
            }
        }

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new DelegateCommand(
                    async (t) =>
                    {
                        var taskItem = t as TaskItem;
                        if (await _taskService.DeleteTaskItemAsync(taskItem!.TaskId))
                        {
                            _taskItems.Remove(taskItem);
                        }
                        DeleteCommand.RaiseCanExecuteChanged();
                    }));
            }
        }

        private DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new DelegateCommand(
                    async (t) =>
                    {
                        if (string.IsNullOrEmpty(TaskName) || string.IsNullOrEmpty(Description))
                        {
                            return;
                        }

                        var oldTaskItem = t as TaskItem;
                        var newTaskItem = new TaskItem
                        {
                            TaskId = oldTaskItem!.TaskId,
                            TaskName = TaskName,
                            Description = Description,
                            CreatedDateTime = oldTaskItem.CreatedDateTime,
                            UpdatedDateTime = DateTime.Now,
                            Deadline = Deadline
                        };

                        if (await _taskService.UpdateTaskItemAsync(newTaskItem))
                        {
                            var idx = _taskItems.ToList().FindIndex(x => x.TaskId == oldTaskItem.TaskId);
                            if (idx != -1)
                            {
                                _taskItems[idx] = newTaskItem;
                            }
                        }
                        //コレクションが変更されることで, DeleteCommandの実行可能判別処理に影響するため,以下呼び出し 
                        DeleteCommand.RaiseCanExecuteChanged();

                        TaskName = string.Empty;
                        Description = string.Empty;
                        Deadline = DateTime.Now;
                    }));
            }
        }
    }
}
