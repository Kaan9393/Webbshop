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

namespace Kladbutiken.Pages.ProductCrud
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Data.MainContext _context;
        private readonly IUserRepository _userRepository;

        public User LoggedInAs { get; set; }

        public IndexModel(DataAccess.Data.MainContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IList<Product> Product { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            var user = _userRepository.GetUserByEmail(userDetailsCookie);
            LoggedInAs = user;

            if (user.Role != "Admin")
            {
                return RedirectToPage("/index");
            }

            LoggedInAs.EmailAddress = user.EmailAddress;

            return Page();
        }
    }
}
