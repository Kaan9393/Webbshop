using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace DataAccess.Entities
{
    public class Address
    {

        public Guid ID { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [MaxLength(50)]
        public string Street { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(50)]
        public string PostalCode { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

    }
}
