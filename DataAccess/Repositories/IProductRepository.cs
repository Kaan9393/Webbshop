using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IProductRepository 
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        Product GetProductById(Guid ID);
        double GetPriceWithDiscount(double price, double discount);
        Task <int> AddProduct(ProductAddModel productModel, /*Category selectedCategory*/ Guid ID);
        IEnumerable<Category> GetAllCategorys();
    }
}