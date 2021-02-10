using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProductsByCategory(string category);
    }
}