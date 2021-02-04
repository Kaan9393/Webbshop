using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public Product Product { get; set; }
    }
}
