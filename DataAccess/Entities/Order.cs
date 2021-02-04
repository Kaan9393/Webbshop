using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DataAccess.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public User UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public IList<Product> ProductList { get; set; }
    }
}
