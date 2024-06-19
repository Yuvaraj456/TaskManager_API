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
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; } //Primary Key for Tasks table

        public string TaskName { get; set; }//Name / title of the task

        public string Description { get; set; }//Description of the task

        public DateTime CreatedOn { get; set; } //Date and time of task creation.

        public int ProjectId { get; set; }//FK, refers to Projects table

        public int CreatedByUserId { get; set; } //FK, refers to AspNetUsers table, indicating who created the task

        public int AssignedToUserId { get; set; } //FK, refers to AspNetUsers table, indicating to whom the task is assigned.


        public int TaskPriorityId { get; set; } //Fk, refers to "TaskPriorities" table

        public DateTime LastUpdatedOn { get; set; } //Date and Time of task last updation

        public string CurrentStatus { get; set; } //Current status name of the task

        public int CurrentTaskStatusId { get; set; }//indicating ID of current task Status

        [NotMapped]
        public string CreatedOnString { get; set; }

        [NotMapped]
        public string LastUpdatedOnString { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual ApplicationUser CreatedByUser { get; set; }

        [ForeignKey("AssignedToUserId")]
        public virtual ApplicationUser AssignedToUser { get; set; }

        [ForeignKey("TaskPriorityId")]
        public virtual TaskPriority TaskPriority { get; set; }

        [ForeignKey("CurrentTaskStatusId")]
        public virtual ICollection<TaskStatusDetail> TaskStatusDetails { get; set; }
    }
}
