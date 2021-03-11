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
        Task AddProduct(ProductAddModel productModel, Guid ID);
        IEnumerable<Category> GetAllCategorys();
        List<Product> GetProductsByList(List<Guid> guids);
    }
}