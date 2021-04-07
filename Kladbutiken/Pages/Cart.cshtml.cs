using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DataAccess.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Kladbutiken.Utils;

namespace Kladbutiken.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItemModel> CartList { get; set; } = new();
        public User LoggedInAs { get; set; }
        public double TotalAmount { get; set; }
        public Address AddressChoice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ShipmentChoice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PaymentChoice { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? AddressID { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = await UserCookieHandler.GetUserByCookie(userDetailsCookie);
            await LoadCart();

            if (AddressID!=null)
            {
                AddressChoice = LoggedInAs.Addresses.FirstOrDefault(a=>a.ID==AddressID);
            }
           
            return Page();
        }

        public async Task<IActionResult> OnPostRemove(Guid id)
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");
            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);

            if (cart != null)
            {
                var product = LoggedInAs.ProductCart.FirstOrDefault(p => p.ID == id);
                LoggedInAs.ProductCart.Remove(product);

                var productIds = LoggedInAs.ProductCart.Select(item => item.ID).ToList();
                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(productIds)); 
            }

            return RedirectToPage("/Cart");
        }

        public async Task<IActionResult> OnPostAdd(Guid id)
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");
            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);

            if (cart != null)
            {
                var product = LoggedInAs.ProductCart.FirstOrDefault(p => p.ID == id);
                LoggedInAs.ProductCart.Add(product);

                var productIds = LoggedInAs.ProductCart.Select(p => p.ID).ToList();
                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(productIds));
            }

            return RedirectToPage("/Cart");
        }

        private async Task LoadCart()
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                LoggedInAs.ProductCart = await UserCookieHandler.GetProductCartByCookie(cart);
            }

            foreach (var product in LoggedInAs.ProductCart)
            {
                if (CartList.Any(c => c.Product.ID == product.ID))
                {
                    var cartItem = CartList.FirstOrDefault(c => c.Product.ID == product.ID);
                    if (cartItem != null) cartItem.Quantity += 1;
                }
                else
                {
                    var cartItem = new CartItemModel { Product = product, Quantity = 1 };
                    CartList.Add(cartItem);
                }
            }

            foreach (var product in LoggedInAs.ProductCart)
            {
                TotalAmount += product.PriceWithDiscount;
            }
        }
    }
}
