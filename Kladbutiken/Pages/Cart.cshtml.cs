using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class CartModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public List<CartItemModel> CartList { get; set; }

        public User LoggedInAs { get; set; }

        public double TotalAmount { get; set; }

        public CartModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            CartList = new();
        }
        public IActionResult OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                LoggedInAs.ProductCart = JsonSerializer.Deserialize<List<Product>>(cart);
            }

            foreach (var product in LoggedInAs.ProductCart)
            {
                if (CartList.Any(c => c.Product.ID == product.ID))
                {
                    var cartItem = CartList.FirstOrDefault(c => c.Product.ID == product.ID);
                    cartItem.Quantity += 1;
                }
                else
                {
                    var cartItem = new CartItemModel() { Product = product, Quantity = 1 };
                    CartList.Add(cartItem);
                }
            }

            foreach (var product in LoggedInAs.ProductCart)
            {
                TotalAmount += product.PriceWithDiscount;
            }

            return Page();
        }

        public IActionResult OnPostRemove(Guid id)
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                LoggedInAs.ProductCart = JsonSerializer.Deserialize<List<Product>>(cart);
            }

            var product = LoggedInAs.ProductCart.FirstOrDefault(p => p.ID == id);
            LoggedInAs.ProductCart.Remove(product);

            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(LoggedInAs.ProductCart));

            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostAdd(Guid id)
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                LoggedInAs.ProductCart = JsonSerializer.Deserialize<List<Product>>(cart);
            }

            var product = LoggedInAs.ProductCart.FirstOrDefault(p => p.ID == id);
            LoggedInAs.ProductCart.Add(product);

            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(LoggedInAs.ProductCart));

            return RedirectToPage("/Cart");
        }
    }
}
