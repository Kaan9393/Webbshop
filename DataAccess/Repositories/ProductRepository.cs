using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            return _context.Products.Include(p => p.Category).AsEnumerable();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category.TypeName == category);
        }

        public Product GetProductById(Guid ID)
        {
            return _context.Products.Include(p => p.Category).Single(p => p.ID == ID);
        }

        public List<Product> GetProductsByList(List<Guid> guids)
        {
            List<Product> products = new();
            foreach (var item in guids)
            {
                var product =_context.Products.Single(p => p.ID == item);
                products.Add(product);
            }
            return products;
        }

        public double GetPriceWithDiscount(double price, double discount)
        {
            return Math.Round(price - (price * (discount / 100)), 0);
        }

        public async Task AddProduct(ProductAddModel productModel, Guid categoryID)
        {
            var selectedCategory = _context.Categories.Single(c => c.ID == categoryID);

            var p = new Product();
            {
                p.ProductName = productModel.ProductName;
                p.URLImg = productModel.URLImg;
                p.Description = productModel.Description;
                p.StockBalance = productModel.StockBalance;
                p.Price = productModel.Price;
                p.Discount = productModel.Discount;
                p.Date=DateTime.Now;
                p.Sales= 0;
                p.Category = selectedCategory;
            };
            _context.Products.Add(p);
            selectedCategory.Products.Add(p);
            await _context.SaveChangesAsync(new CancellationToken());
        }

        public IEnumerable<Category> GetAllCategorys()
        {
            return _context.Categories.AsEnumerable();
        }
    }
}


