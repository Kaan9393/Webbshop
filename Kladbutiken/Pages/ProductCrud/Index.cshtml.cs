using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Entities;
using Kladbutiken.Utils;
using Microsoft.AspNetCore.Http;

namespace Kladbutiken.Pages.ProductCrud
{
    public class IndexModel : PageModel
    {
        private readonly MainContext _context;

        public IList<Product> Products { get;set; }
        public User LoggedInAs { get; set; }

        public IndexModel(MainContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();

            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");
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
