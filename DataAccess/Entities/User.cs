using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class User
    {
        public int ID { get; set; }

        [MaxLength(13,ErrorMessage ="SSN must be 12 characters YYYYMMDD-XXXX"), MinLength(13)]
        public string SSN { get; set; }

        [Required]
        [MinLength(8,ErrorMessage ="Password must be atleast 8 characters and less than 50 "),MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public Address Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public DateTime RegisterDate { get; set; }

        public IList<Product> ProductCart { get; set; }

    }
}
