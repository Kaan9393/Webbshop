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

        public IEnumerable<Product> GetProductsBySearch(string search)
        {
            //return _context.Products.Where(p => p.ProductName.Contains(search)).Include(p => p.Category.TypeName.Contains(search));
            return _context.Products.Where(p => p.ProductName.Contains(search));
        }
        public IEnumerable<Product> GetCategoriesBySearch(string search)
        {
            return _context.Products.Where(c => c.Category.TypeName.Contains(search));
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
            return guids.Select(item => _context.Products.FirstOrDefault(p => p.ID == item)).ToList();
        }

        public double GetPriceWithDiscount(double price, double discount)
        {
            return Math.Round(price - (price * (discount / 100)), 0);
        }

        public async Task AddProduct(ProductAddModel productModel, Guid categoryID)
        {
            var selectedCategory = _context.Categories.Single(c => c.ID == categoryID);

            var p = new Product
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
            foreach (var item in orderModel.ProductList)
            {
                item.Product.Sales += item.Quantity;
                item.Product.StockBalance -= item.Quantity;

                _context.Products.Attach(item.Product).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }
        public List<Product> GetMostSoldProducts()
        {
            return _context.Products.OrderByDescending(p => p.Sales).Take(3).ToList();
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


