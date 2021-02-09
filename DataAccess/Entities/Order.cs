﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Order
    {
        public int ID { get; set; }

        [Required]
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public IList<Product> ProductList { get; set; }
    }
}
