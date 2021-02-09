using DataAccess.Data;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMainContext _context;

        public ProductRepository(IMainContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            /*using (var db = new MainContext())
            {
                var products = db.Products.Where(p => p.Category.TypeName == category);

                return products;
            }*/
            return _context.Products.Where(p => p.Category.TypeName == category);
        }
    }
}
