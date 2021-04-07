using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Kladbutiken.Pages
{
    public class SearchPageModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public List<Product> AllProducts { get; set; }
        public List<Category> AllCategories { get; set; }
        public List<Product> AllSelectedProducts { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public User LoggedInAs { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchInput { get; set; }

        public List<Product> ProductOutput { get; set; }

        public List<Product> CategoryOutput { get; set; }
        public SearchPageModel(IUserRepository userRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
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

            ProductOutput = _productRepository.GetProductsBySearch(SearchInput).ToList();
            CategoryOutput = _productRepository.GetCategoriesBySearch(SearchInput).ToList();

            AllProducts = _productRepository.GetAllProducts().ToList();
            AllCategories = _categoryRepository.GetAllCategorys().ToList();
            AllSelectedProducts = _productRepository.GetProductsByCategory(SelectedCategory).ToList();


        }
    }
}
