using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class CartModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public User LoggedInAs { get; set; }

        public double TotalAmount { get; set; }

        public CartModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

            foreach (var product in LoggedInAs.ProductCart)
            {
                TotalAmount += product.Price;
            }

            return Page();
        }
    }
}
