using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public DateTime RegisterDate { get; set; }
        public IList<Product> ProductCart { get; set; }

    }
}
