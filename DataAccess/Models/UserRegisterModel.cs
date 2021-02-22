using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserRegisterModel
    {
        [Required/*(*//*ErrorMessage = "This field is required.")*/]
        public string FirstName { get; set; }

        [Required/*(*//*ErrorMessage ="This field is required.")*/]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Lösenordet måste innehålla minst 8 och max 50 tecken"), MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
