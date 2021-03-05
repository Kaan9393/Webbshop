using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class CartItem
    {

        public Guid ID { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

    }
}
