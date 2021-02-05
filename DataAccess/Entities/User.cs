using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(13,ErrorMessage ="SSN must be 12 characters YYYY-MMDDXXXX"), MinLength(13)]
        public string SSN { get; set; }// Lagt till personnummer
        [Required]
        [MinLength(8,ErrorMessage ="Password must be atleast 8 characters and less than 50 "),MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }// Ändrade till string
        [Required]
        public string EmailAddress { get; set; }// länka till address ?
        public DateTime RegisterDate { get; set; }
        public IList<Product> ProductCart { get; set; }

    }
}
