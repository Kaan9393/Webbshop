using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CartPaymentModel
    {
        [Required]
        public Address AddressChoice { get; set; }

        [Required]
        public int ShipmentChoice { get; set; }

        [Required]
        public int PaymentChoice { get; set; }
    }
}
