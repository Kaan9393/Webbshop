using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;
using Kladbutiken.Utils;
using Microsoft.AspNetCore.Http;

namespace Kladbutiken.Pages.CategoryCrud
{
    public class CreateModel : PageModel
    {
        private readonly MainContext _context;

        public CreateModel(MainContext context)
        {
            _context = context;
        }
        public User LoggedInAs { get; set; }

        public async Task<IActionResult>OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            if (userDetailsCookie == null)
            {
                return RedirectToPage("Login");
            }
            var cart = HttpContext.Session.GetString("cart");
            LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie,cart);
            if (LoggedInAs.Role != "Admin")
            {
                return RedirectToPage("/index");
            }

            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
