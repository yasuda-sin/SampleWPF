using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWPF.Models;

namespace SampleWPF.Services
{
    public interface ITaskService
    {
        ObservableCollection<TaskItem> GetTasks();
        void AddTask(TaskItem item);
        void RemoveTask(TaskItem item);
        void UpdateTask(TaskItem item);
    }
}
