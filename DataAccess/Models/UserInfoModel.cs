using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserInfoModel
    {
        public string Role { get; set; }

        [MaxLength(13, ErrorMessage = "Personnummer måste innehålla 12 siffror (YYYYMMDD-XXXX)"), MinLength(13)]
        public string SSN { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

    }
}
