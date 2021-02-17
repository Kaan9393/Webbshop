using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        Product GetProductById(Guid ID);
    }
}