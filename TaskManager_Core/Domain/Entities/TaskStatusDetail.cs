using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity;

namespace TaskManager_Core.Domain.Entities
{
    public class TaskStatusDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public int TaskStatusId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime StatusUpdationDateTime { get; set; }

        [NotMapped]
        public string StatusUpdationDateTimeString { get; set; }

        [ForeignKey("TaskStatusId")]
        public virtual TaskStatus TaskStatus { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}
