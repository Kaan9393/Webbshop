using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DataAccess.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Kladbutiken.Pages
{
    public class CartModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public List<CartItemModel> CartList { get; set; }

        public User LoggedInAs { get; set; }

        public double TotalAmount { get; set; }

        public Address AddressChoice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ShipmentChoice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PaymentChoice { get; set; }

        [BindProperty]
        public PaymentModel PaymentModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? AdressID { get; set; }

        public CartModel(IUserRepository userRepository, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
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
            LoadCart();
            if (AdressID!=null)
            {
                AddressChoice = LoggedInAs.Addresses.FirstOrDefault(a=>a.ID==AdressID);
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
                LoggedInAs.ProductCart = _productRepository.GetProductsByList(JsonSerializer.Deserialize<List<Guid>>(cart));
            }

            var product = LoggedInAs.ProductCart.FirstOrDefault(p => p.ID == id);
            LoggedInAs.ProductCart.Remove(product);

            List<Guid> productIds = new();
            foreach (var item in LoggedInAs.ProductCart)
            {
                productIds.Add(item.ID);
            }

            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(productIds));

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
                LoggedInAs.ProductCart = _productRepository.GetProductsByList(JsonSerializer.Deserialize<List<Guid>>(cart));

                var product = LoggedInAs.ProductCart.FirstOrDefault(p => p.ID == id);
                LoggedInAs.ProductCart.Add(product);


                List<Guid> productIds = LoggedInAs.ProductCart.Select(p => p.ID).ToList();

                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(productIds));
            }

            return RedirectToPage("/Cart");
        }
        public void LoadCart()
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                var productIds = JsonSerializer.Deserialize<List<Guid>>(cart);
                LoggedInAs.ProductCart = _productRepository.GetProductsByList(productIds);
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
        }
    }
}
