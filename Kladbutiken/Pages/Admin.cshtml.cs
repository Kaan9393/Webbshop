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
    public class AdminModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public AdminModel(IUserRepository userRepository, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public string LoggedInAs { get; set; }
        public IEnumerable<Product> AllProducts { get; set; }

        public IActionResult OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            var user = _userRepository.GetUserByEmail(userDetailsCookie);

            if (user.Role != "Admin")
            {
                return RedirectToPage("/index");
            }

            LoggedInAs = user.EmailAddress;
            AllProducts = _productRepository.GetAllProducts();

            return Page();
        }

        
        public IActionResult OnPostLogout()
        {
            Response.Cookies.Delete("UserDetails");

            return RedirectToPage("/index");
        }
    }
}
