using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Order
    {
        public Guid ID { get; set; }

        [Required]
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } //Koppla till enum.

        public List<Product> ProductList { get; set; }

        public Order()
        {
            ID = Guid.NewGuid();
            ProductList = new();
        }
    }
}
