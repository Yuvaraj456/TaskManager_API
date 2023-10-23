using System.ComponentModel.DataAnnotations;

namespace TaskManager_Core.Domain.Entities
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateTime DateOfStart { get; set; }

        public int TeamSize { get; set; }
    }

}
