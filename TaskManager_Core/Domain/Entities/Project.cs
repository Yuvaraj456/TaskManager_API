using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager_Core.Domain.Entities
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        [DisplayFormat(DataFormatString = "d/M/yyyy")]
        public DateTime DateOfStart { get; set; }

        public int? TeamSize { get; set; }

        public bool Active { get; set; }

        public string Status { get; set; }

        public int ClientLocationId { get; set; }

        [ForeignKey("ClientLocationId")]
        public virtual ClientLocation? ClientLocation { get; set; }

    }

}
