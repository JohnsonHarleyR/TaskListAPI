using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList.Crosscutting.Models
{
    public class TaskModel
    {
        public int? Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public required string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
