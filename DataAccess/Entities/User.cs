using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public class User
    {
        public Guid ID { get; set; }

        public string Role { get; set; }

        [MaxLength(13,ErrorMessage ="Personnummer måste innehålla 12 siffror (YYYYMMDD-XXXX)"), MinLength(13)]
        public string SSN { get; set; }

        [Required]
        [MinLength(8,ErrorMessage ="Lösenordet måste innehålla minst 8 och max 50 tecken"),MaxLength(50)]
        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public List<Address> Addresses { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public DateTime RegisterDate { get; set; }

        public List<Product> ProductCart { get; set; }

        public List<Order> Orders { get; set; }

        public User()
        {
            ID = Guid.NewGuid();
            ProductCart = new();
            Orders = new();
            Addresses = new();
        }

    }
}
