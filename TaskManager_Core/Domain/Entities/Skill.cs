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
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }

        public string SkillName { get; set; }

        public string SkillLevel { get; set; }

  
        public int Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
