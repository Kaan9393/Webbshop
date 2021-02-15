using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
    }
}