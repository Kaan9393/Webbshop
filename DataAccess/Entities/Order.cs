using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public class Order
    {
        public Guid ID { get; set; }

        public string OrderNumber { get; set; }

        [Required]
        [JsonIgnore]
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } //Koppla till enum.

        public List<CartItem> ProductList { get; set; } = new();

        public string Address { get; set; }

        public int ShipmentChoice { get; set; }

        public int PaymentChoice { get; set; }

        /*public Order()
        {
            ID = Guid.NewGuid();
            ProductList = new();
        }*/
    }
}
