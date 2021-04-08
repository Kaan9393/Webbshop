using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Kladbutiken.Utils;

namespace Kladbutiken.Pages.CategoryCrud
{
    public class EditModel : PageModel
    {
        private readonly MainContext _context;
        public EditModel(MainContext context)
        {
            _context = context;
        }
        public User LoggedInAs { get; set; }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            if(userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }
            var cart = HttpContext.Session.GetString("cart");

            LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);

            if(LoggedInAs.Role != "Admin")
            {
                return RedirectToPage("/index");
            }

            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.ID == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
