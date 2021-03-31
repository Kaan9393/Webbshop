using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class OrderModel
    {
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public List<CartItemModel> ProductList { get; set; } = new List<CartItemModel>();

        public Address ShippingAddress { get; set; }

        public int ShipmentChoice { get; set; }

        public int PaymentChoice { get; set; }

    }
}
