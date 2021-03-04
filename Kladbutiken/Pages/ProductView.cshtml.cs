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

namespace Kladbutiken.Pages
{
    public class ProductViewModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        [BindProperty (SupportsGet = true)]
        public Guid SelectedProduct { get; set; }

        public Product Product { get; set; }

        public List<Product> MatchingProducts { get; set; }
        
        public List<Category> AllCategories { get; set; }

        public User LoggedInAs { get; set; }

        public ProductViewModel(IProductRepository productRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult OnGet()
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

            Product = _productRepository.GetProductById(SelectedProduct);
            AllCategories = _categoryRepository.GetAllCategorys().ToList();


            MatchingProducts = _productRepository.GetProductsByCategory(Product.Category.TypeName).ToList();
            foreach (var matchedProduct in MatchingProducts)
            {
                matchedProduct.PriceWithDiscount = _productRepository.GetPriceWithDiscount(matchedProduct.Price,matchedProduct.Discount);
            }

            Product.PriceWithDiscount = _productRepository.GetPriceWithDiscount(Product.Price, Product.Discount);

            return Page();
        }

        public IActionResult OnPostAdd(Guid id)
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            AllCategories = _categoryRepository.GetAllCategorys().ToList();

            if (userDetailsCookie != null)
            {

                LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

                Product = _productRepository.GetProductById(id);

                MatchingProducts = _productRepository.GetProductsByCategory(Product.Category.TypeName).ToList();
                foreach (var matchedProduct in MatchingProducts)
                {
                    matchedProduct.PriceWithDiscount = _productRepository.GetPriceWithDiscount(matchedProduct.Price, matchedProduct.Discount);
                }

                Product.PriceWithDiscount = _productRepository.GetPriceWithDiscount(Product.Price, Product.Discount);

                var cart = HttpContext.Session.GetString("cart");
                if (cart != null)
                {
                    LoggedInAs.ProductCart = _productRepository.GetProductsByList(JsonSerializer.Deserialize<List<Guid>>(cart));
                    LoggedInAs.ProductCart.Add(Product);
                    List<Guid> productIds = new();
                    foreach (var product in LoggedInAs.ProductCart)
                    {
                        productIds.Add(product.ID);
                    }
                    HttpContext.Session.SetString("cart", JsonSerializer.Serialize(productIds));
                }
                else
                {
                    List<Guid> productIds = new();
                    productIds.Add(Product.ID);
                    HttpContext.Session.SetString("cart", JsonSerializer.Serialize(productIds));
                }
            }
            else
            {
                return RedirectToPage("/Login");
            }
            return Redirect("/ProductView?SelectedProduct=" + id);
        }
    }
}
