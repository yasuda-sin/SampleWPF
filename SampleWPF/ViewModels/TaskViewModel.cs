using SampleWPF.Models;
using SampleWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleWPF.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        private readonly ITaskService _taskService;
        public ObservableCollection<TaskItem> TaskItems { get; set; }

        public required string TaskName { get; set; }
        public required string Description { get; set; }
        public DateTime Deadline { get; set; } = DateTime.Now;

        public ICommand AddTaskCommand { get; set; }
        public ICommand RemoveTaskCommand { get; set; }

        public TaskViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            TaskItems = new ObservableCollection<TaskItem>(_taskService.GetTasks());
            AddTaskCommand = new RelayCommand(AddTask);
            RemoveTaskCommand = new RelayCommand<TaskItem>(RemoveTask);
        }

        private void AddTask()
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                var newTask = new TaskItem
                {
                    TaskName = TaskName,
                    Description = Description,
                    Deadline = Deadline
                };
                _taskService.AddTask(newTask);
                TaskItems.Add(newTask);     //viewに反映

                TaskName =string.Empty;
                Description = string.Empty;
                Deadline = DateTime.Now;
            }
        }

        private  void RemoveTask(TaskItem item)
        {
            _taskService.RemoveTask(item);
            TaskItems.Remove(item);
        }
    }
}
