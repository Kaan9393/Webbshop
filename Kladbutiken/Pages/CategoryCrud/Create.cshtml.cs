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

namespace Kladbutiken.Pages.CategoryCrud
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.Data.MainContext _context;

        public CreateModel(MainContext context)
        {
            _context = context;
        }
        public User LoggedInAs { get; set; }

        public async Task<IActionResult>OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            LoggedInAs = await UserCookieHandler.GetUserByCookie(userDetailsCookie);

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
