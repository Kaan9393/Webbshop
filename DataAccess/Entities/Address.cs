using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Address
    {
        public int ID { get; set; }
        public User UserID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

    }
}
