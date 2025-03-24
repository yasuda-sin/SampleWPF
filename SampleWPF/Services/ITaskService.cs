using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWPF.Models;

namespace SampleWPF.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetTasks();
        void AddTask(TaskItem item);
        void RemoveTask(TaskItem item);
    }
}
