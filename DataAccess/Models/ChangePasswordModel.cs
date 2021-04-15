using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Detta fältet krävs.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Detta fältet krävs.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Detta fältet krävs.")]
        [Compare("NewPassword", ErrorMessage = "Lösenorden matchar inte.")]
        public string ConfirmPassword { get; set; }
    }
}
