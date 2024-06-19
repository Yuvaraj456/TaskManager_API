using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager_Core.DTO
{
    public class AuthenticationResponse
    {

        public string? Token { get; set; }
            
        public string? Email { get; set; }

        public string? Role { get; set; }

        public DateTime Expiration { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpirationDateTime { get; set; }


    }   
}
