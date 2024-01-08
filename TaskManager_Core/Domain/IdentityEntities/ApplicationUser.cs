

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager_Core.Domain.Entities;
using TaskManager_Core.DTO;

namespace TaskManager.Identity
{
    public class ApplicationUser:IdentityUser<int>
    {
        [NotMapped]
        public string Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string CountryId { get; set; }

        public bool ReceivesNewsLetter { get; set; }

    }
}
