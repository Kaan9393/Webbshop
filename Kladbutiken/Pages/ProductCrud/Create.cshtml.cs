using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Data;
using DataAccess.Repositories;
using DataAccess.Entities;
using DataAccess.Models;

namespace Kladbutiken.Pages.ProductCrud
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _productrepository;
        private readonly IMainContext IMainContext;
        private readonly ICategoryRepository _categoryRepository;

        //private readonly DataAccess.Data.MainContext _Context;

        public List<SelectListItem> Categories { get; set; } = new();

        [BindProperty]
        public ProductAddModel Product { get; set; }//gör en product model

        [BindProperty]
        public Guid CategoryId { get; set; }
        public CreateModel(IProductRepository productrepository, IMainContext mainContext,ICategoryRepository categoryRepository)
        {
            _productrepository = productrepository;
             IMainContext = mainContext;
            _categoryRepository= categoryRepository;
        }
        //public CreateModel(DataAccess.Data.MainContext context)
        //{
        //    _context = context;
        //}

        public IActionResult OnGet()
        {
            var categories =_productrepository.GetAllCategorys().ToList();  
            foreach (var category in categories)
            {
                Categories.Add(new SelectListItem { Value=category.ID.ToString(), Text=category.TypeName });
            }
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //var selectedCategory = IMainContext.Categories.Single(c => c.ID == CategoryId);
            //Product.Date = DateTime.Now;
            //Product.Sales = 0;
            //Product.Category = selectedCategory;
            //selectedCategory.Products.Add(Product);


            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Products.Add(Product);
            await _productrepository.AddProduct(Product,CategoryId/*)selectedCategory*/);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
