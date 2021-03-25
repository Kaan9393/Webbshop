using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DataAccess.Repositories;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Kladbutiken.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public List<Product> AllProducts { get; set; }
        public List<Category> AllCategories { get; set; }
        public List<Product> AllSelectedProducts { get; set; }
        public User LoggedInAs { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool FullProductView { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SelectedCategory { get; set; }
        public List<Product> TopSalesTop5 { get; set; }
        public List<Product> LatestArrivals { get; set; }
        public List<Product> DiscountedProducts { get; set; }


        public IndexModel(ILogger<IndexModel> logger, IUserRepository userRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public void OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            if (userDetailsCookie != null)
            {
                LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

                var cart = HttpContext.Session.GetString("cart");
                if (cart != null)
                {
                    LoggedInAs.ProductCart = _productRepository.GetProductsByList(JsonSerializer.Deserialize<List<Guid>>(cart));
                }
            }
            try
            {
                TopSalesTop5 = _productRepository.GetMostSoldProducts();
                LatestArrivals = _productRepository.GetLatestArrivals();
                DiscountedProducts = _productRepository.GetDiscountedProducts();

            }
            catch (Exception)
            {

                throw;
            }

            
            _userRepository.CheckForAdmin();
            AllProducts = _productRepository.GetAllProducts().ToList();
            AllCategories = _categoryRepository.GetAllCategorys().ToList();
            AllSelectedProducts = _productRepository.GetProductsByCategory(SelectedCategory).ToList();
        }
        
    }
}
