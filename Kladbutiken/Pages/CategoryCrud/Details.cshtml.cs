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
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Data.MainContext _context;
        private readonly IUserRepository _userRepository;
        public DetailsModel(DataAccess.Data.MainContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public User LoggedInAs { get; set; }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.ID == id);
            if (Category == null)
            {
                return NotFound();
            }


            return Page();

        }
    }
}
