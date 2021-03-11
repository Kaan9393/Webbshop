using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class Category
    {
        public Guid ID { get; set; }

        public List<Product> Products { get; set; } = new();

        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; }

        public Category()
        {
            ID = Guid.NewGuid();
            
        }
    }
}
