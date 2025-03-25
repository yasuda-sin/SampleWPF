using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWPF.Models;

namespace SampleWPF.Services
{
    public class TaskService : ITaskService
    {
        //private readonly List<TaskItem> _taskItems = new List<TaskItem>();
        //public List<TaskItem> GetTasks() => _taskItems;
        
        private readonly ObservableCollection<TaskItem> _taskItems = new ObservableCollection<TaskItem>();
        public ObservableCollection<TaskItem> GetTasks() => _taskItems;
        public void AddTask(TaskItem task) => _taskItems.Add(task);
        public void RemoveTask(TaskItem task) => _taskItems.Remove(task);
        public void UpdateTask(TaskItem task)
        {
            var existingTask = _taskItems.FirstOrDefault(t => t.TaskName == task.TaskName);
            if(existingTask != null)
            {
                existingTask.Description = task.Description;
                existingTask.Status = task.Status;
                existingTask.UpdatedDateTime = DateTime.Now;
            }
        }
    }
}
