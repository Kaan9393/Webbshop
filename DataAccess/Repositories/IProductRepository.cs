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
        void UpdateSaldo(OrderModel orderModel);
        List<Product> GetMostSoldProducts();
        List<Product> GetLatestArrivals();
        List<Product> GetDiscountedProducts();
        IEnumerable<Product> GetProductsBySearch(string search);
        IEnumerable<Product> GetCategoriesBySearch(string search);
    }
}