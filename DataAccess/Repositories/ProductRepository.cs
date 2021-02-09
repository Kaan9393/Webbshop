using DataAccess.Data;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public static class ProductRepository
    {
        public static IEnumerable<Product> GetProductsByCategory(string category)
        {
            using (var db = new MainContext())
            {
                var products = db.Products.Where(p => p.Category.TypeName == category);

                return products;
            }
        }
    }
}
