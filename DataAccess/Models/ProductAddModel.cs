using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ProductAddModel
    {
        public Guid ID { get; set; }

        [Required]
        public Category Category { get; set; }
        [Required(ErrorMessage ="Product must have a type")]
        public string ProductName { get; set; }

        public string URLImg { get; set; }
        [Required(ErrorMessage ="Product must have a price")]
        public double Price { get; set; }

        public int StockBalance { get; set; }

        public double Discount { get; set; }

        public string Description { get; set; }

        
    }
}
