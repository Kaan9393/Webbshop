using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Repositories;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Kladbutiken.Utils;

namespace Kladbutiken.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public List<Product> AllProducts { get; set; }
        public List<Category> AllCategories { get; set; }
        public List<Product> AllSelectedProducts { get; set; }
        public List<Product> TopSalesTop5 { get; set; }
        public List<Product> LatestArrivals { get; set; }
        public List<Product> DiscountedProducts { get; set; }
        public User LoggedInAs { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool FullProductView { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }


        public IndexModel(IUserRepository userRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");

            if (userDetailsCookie != null)
            {
                LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);
            }
            
            TopSalesTop5 = _productRepository.GetMostSoldProducts();
            LatestArrivals = _productRepository.GetLatestArrivals();
            DiscountedProducts = _productRepository.GetDiscountedProducts();

            _userRepository.CheckForAdmin();
            AllProducts = _productRepository.GetAllProducts().ToList();
            AllCategories = _categoryRepository.GetAllCategorys().ToList();
            AllSelectedProducts = _productRepository.GetProductsByCategory(SelectedCategory).ToList();
        }

    }
}
