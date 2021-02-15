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

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.AsEnumerable();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category.TypeName == category);
        }
    }
}
