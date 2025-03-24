using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWPF.Models;

namespace SampleWPF.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _taskItems = new List<TaskItem>();
        public List<TaskItem> GetTasks() =>  _taskItems;

        public void AddTask(TaskItem item)
        {
            _taskItems.Add(item);
        }
        public void RemoveTask(TaskItem item)
        {
            _taskItems.Remove(item);
        }
    }
}
