using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWPF.Models
{
    public enum TaskStatus
    {
        NotStarted = 0,
        InProgress,
        Completed
    }

    public class TaskItem
    {
        public required  string TaskName { get; set; }
        public required string Description { get; set; }
        public int Status { get; set; } = (int)TaskStatus.NotStarted;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime UpdatedDateTime { get; set; }
        public DateTime Deadline { get; set; }
    }
}
