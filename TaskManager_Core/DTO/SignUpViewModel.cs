using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager_Core.Domain.Entities;

namespace TaskManager_Core.DTO
{
    public class SignUpViewModel
    {
        public PersonFullName PersonName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string DateOfBirth { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Gender { get; set; }

        public string CountryId { get; set; }

        public bool ReceivesNewsLetter { get; set; }
         
        public List<Skill> Skills { get; set; }
            
    }

    public class PersonFullName
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
