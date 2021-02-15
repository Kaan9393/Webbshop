using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 characters and less than 50 "), MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 characters and less than 50 "), MaxLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
