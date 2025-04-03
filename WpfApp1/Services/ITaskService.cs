using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    internal interface ITaskService
    {
        Task<List<TaskItem>> GetAllTaskItemsAsync();
        Task<TaskItem?> GetTaskItemByIdAsync(int taskId);
        Task<bool> AddTaskItemAsync(TaskItem taskItem);
        Task<bool> UpdateTaskItemAsync(TaskItem taskItem);
        Task<bool> DeleteTaskItemAsync(int taskId);
    }
}
