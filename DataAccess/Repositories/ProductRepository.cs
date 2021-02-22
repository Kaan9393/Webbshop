using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository, ICategoryRepository
    {
        private readonly IMainContext _context;
        private readonly MainContext _mainContext;//

        public ProductRepository(IMainContext context, MainContext mainContext)//)
        {
            _context = context;
            _mainContext = mainContext;
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

        public double GetPriceWithDiscount(double price, double discount)
        {
            return price - (price * (discount / 100));
        }
        public Task <int> AddProduct(ProductAddModel productModel/* Category selectedCategory*/, Guid categoryID)
        {
            var selectedCategory = _context.Categories.Single(c => c.ID == categoryID);

            var p = new Product();
            {
                p.ProductName = productModel.ProductName;
                p.Description = productModel.Description;
                p.Price = productModel.Price;
                p.Date=DateTime.Now;
                p.Sales= 0;
                p.Category = selectedCategory;
            };
            //selectedCategory.Products.Add(p);
            //_mainContext.Products.Add(p);
            _context.Products.Add(p);
            AddProductToCategory(p, selectedCategory);
            Task <int> task=_context.SaveChangesAsync(new CancellationToken());
            //_mainContext.SaveChanges();//save changes();?
            return task;
        }

        private static void AddProductToCategory(Product productModel, Category selectedCategory)
        {
            try
            {
                selectedCategory.Products.Add(productModel);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Category> GetAllCategorys()
        {
            //return _mainContext.Categories.AsEnumerable();
            return _context.Categories.AsEnumerable();

        }


    }
}


