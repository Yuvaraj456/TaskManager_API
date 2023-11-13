using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager_Core.Domain.Entities;

namespace TaskManager_Core.DTO
{
    public class ProjectViewModel
    {
  
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime DateOfStart { get; set; }
        public int? TeamSize { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public int ClientLocationId { get; set; }
        public ClientLocation ClientLocation { get; set; }
    }
}
