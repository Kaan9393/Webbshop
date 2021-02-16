using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Data;
using DataAccess.Entities;

namespace Kladbutiken.Pages.ProductCrud
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.Data.MainContext _context;

        public List<SelectListItem> Categories { get; set; } = new();

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public Guid CategoryId { get; set; }

        public CreateModel(DataAccess.Data.MainContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var categories = _context.Categories.ToList();
            foreach (var category in categories)
            {
                Categories.Add(new SelectListItem { Value=category.ID.ToString(), Text=category.TypeName });
            }
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var selectedCategory = _context.Categories.Single(c => c.ID == CategoryId);
            Product.Date = DateTime.Now;
            Product.Sales = 0;
            Product.Category = selectedCategory;
            selectedCategory.Products.Add(Product);


            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
