using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
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

        public User LoggedInAs { get; set; }

        public IEnumerable<Product> AllProducts { get; set; }

        public IActionResult OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs  = _userRepository.GetUserByEmail(userDetailsCookie);

            if (LoggedInAs.Role != "Admin")
            {
                return RedirectToPage("/index");
            }

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
