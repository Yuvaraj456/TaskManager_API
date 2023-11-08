using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager_Core.Domain.Entities
{
    public class ClientLocation
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption:DatabaseGeneratedOption.Identity)]
        public int ClientLocationId { get; set; }

        public string ClientLocationName { get; set;}
    }
}
