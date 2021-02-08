using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; }
    }
}
