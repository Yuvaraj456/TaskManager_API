

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Identity
{
    public class ApplicationUser:IdentityUser<int>
    {
        [NotMapped]
        public string Role { get; set; }
    }
}
