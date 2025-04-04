using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public enum TaskStatus
    {
        NotStarted = 0,
        InProgress,
        Completed
    }

    public class TaskItem
    {
        public int TaskId { get; set; }
        public required string TaskName { get; set; }
        public required string Description { get; set; }
        public byte Status { get; set; } = (byte)TaskStatus.NotStarted;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime UpdatedDateTime { get; set; }
        public DateTime Deadline { get; set; }

        public TaskItem()
        {
            TaskName = string.Empty;
            Description = string.Empty;
        }
    }
}
