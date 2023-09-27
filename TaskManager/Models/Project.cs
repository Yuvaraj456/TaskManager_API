using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
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
