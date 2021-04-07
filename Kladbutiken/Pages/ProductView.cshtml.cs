using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Kladbutiken.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class ProductViewModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        [BindProperty (SupportsGet = true)]
        public Guid SelectedProduct { get; set; }

        public Product Product { get; set; }
        public List<Product> MatchingProducts { get; set; }
        public List<Category> AllCategories { get; set; }
        public User LoggedInAs { get; set; }

        public ProductViewModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");

            if (userDetailsCookie != null)
            {
                LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);
            }

            Product = _productRepository.GetProductById(SelectedProduct);
            AllCategories = _categoryRepository.GetAllCategorys().ToList();
            MatchingProducts = _productRepository.GetProductsByCategory(Product.Category.TypeName).ToList();

            return Page();
        }

        // Lägg till i varukorg klickad
        public async Task<IActionResult> OnPostAdd(Guid id)
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            AllCategories = _categoryRepository.GetAllCategorys().ToList();

            if (userDetailsCookie != null)
            {
                LoggedInAs = await UserCookieHandler.GetUserByCookie(userDetailsCookie);

                Product = _productRepository.GetProductById(id);
                MatchingProducts = _productRepository.GetProductsByCategory(Product.Category.TypeName).ToList();

                var cart = HttpContext.Session.GetString("cart");
                if (cart != null)
                {
                    var productCart = await UserCookieHandler.GetProductCartByCookie(cart);
                    productCart.Add(Product);
                    var productIds = productCart.Select(product => product.ID).ToList();
                    HttpContext.Session.SetString("cart", JsonSerializer.Serialize(productIds));
                }
                else
                {
                    var productIds = new List<Guid> {Product.ID};
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
