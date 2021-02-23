using DataAccess.Entities;
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
        public Guid UserID { get; set; }

        [Required/*(*//*ErrorMessage = "This field is required.")*/]
        public string FirstName { get; set; }

        [Required/*(*//*ErrorMessage ="This field is required.")*/]
        public string LastName { get; set; }

        [MaxLength(13, ErrorMessage = "Personnummer måste innehålla 12 siffror (YYYYMMDD-XXXX)"), MinLength(6)]
        public string SSN { get; set; }

        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }

    }
}
