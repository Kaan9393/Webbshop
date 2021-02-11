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
        public IEnumerable<Category> Categorys { get; set; }

        public CreateModel(DataAccess.Data.MainContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Categorys = _context.Categories.AsEnumerable();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
