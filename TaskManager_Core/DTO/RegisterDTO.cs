﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager_Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "PersonName is required")]
        public string PersonName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email addressformat")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "PhoneNumber is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "PhoneNumber should contain number only")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}
