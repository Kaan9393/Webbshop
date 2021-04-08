using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Kladbutiken.Utils;

namespace Kladbutiken.Pages.CategoryCrud
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Data.MainContext _context;

        public IndexModel(DataAccess.Data.MainContext context)
        {
            _context = context;
        }
        public User LoggedInAs { get; set; }

        public IList<Category> Categories { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            var cart = HttpContext.Session.GetString("cart");
            var userDetailsCookie = Request.Cookies["UserDetails"];
            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);

            if (LoggedInAs.Role != "Admin")
            {
                return RedirectToPage("/index");
            }

            return Page();
        }
    }
}
