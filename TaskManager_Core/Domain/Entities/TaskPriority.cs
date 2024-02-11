using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager_Core.Domain.Entities
{
    public class TaskPriority
    {
        [Key]        
        public int TaskPriorityId { get; set; }

        public string TaskPriorityName { get; set; }
    }
}
