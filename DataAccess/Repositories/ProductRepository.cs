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
                var product =_context.Products.FirstOrDefault(p => p.ID == item);
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

            var p = new Product()
            {
                ID = Guid.NewGuid(),
                ProductName = productModel.ProductName,
                URLImg = productModel.URLImg,
                Description = productModel.Description,
                StockBalance = productModel.StockBalance,
                Price = productModel.Price,
                Discount = productModel.Discount,
                Date = DateTime.Now,
                Sales = 0,
                Category = selectedCategory
            };
            _context.Products.Add(p);
            selectedCategory.Products.Add(p);
            await _context.SaveChangesAsync(new CancellationToken());
        }

        public IEnumerable<Category> GetAllCategorys()
        {
            return _context.Categories.AsEnumerable();
        }
        public void UpdateSaldo(OrderModel orderModel)
        {
            var products = _context.Products.ToList();

                foreach (var item in orderModel.ProductList)
                {
                    var saldo = products.FirstOrDefault(p => p.ID == item.Product.ID);
                    saldo.StockBalance -= item.Quantity;
                    saldo.Sales += item.Quantity;
                }
            
            _context.SaveChanges();
        }
        public List<Product> GetMostSoldProducts()
        {
            return _context.Products.OrderByDescending(p=>p.Sales).Take(3).ToList();
        }
        public List<Product> GetLatestArrivals()
        {
            return _context.Products.OrderByDescending(p => p.Date).Take(3).ToList();
        }
        public List<Product> GetDiscountedProducts()
        {
            return _context.Products.OrderByDescending(p => p.Discount).Take(3).ToList();
        }
    }
}


