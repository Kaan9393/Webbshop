using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Kladbutiken.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public User LoggedInAs { get; set; }
        public OrderModel OrderModel { get; set; } = new();
        public Order Order { get; set; }
        public Address Address { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ShipmentChoice { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int PaymentChoice { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid AddressID { get; set; }


        public PaymentModel(IOrderRepository orderRepository, IProductRepository productRepository, ICartItemRepository cartItemRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _cartItemRepository = cartItemRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");
            if (userDetailsCookie != null)
            {
                LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);

                if (cart != null)
                {
                    OrderModel.User = LoggedInAs;
                    foreach (var product in LoggedInAs.ProductCart)
                    {
                        if (OrderModel.ProductList.Any(c => c.Product.ID == product.ID))
                        {
                            var cartItem = OrderModel.ProductList.FirstOrDefault(c => c.Product.ID == product.ID);
                            if (cartItem != null) cartItem.Quantity += 1;
                        }
                        else
                        {
                            var cartItem = new CartItemModel { Product = product, Quantity = 1 };
                            OrderModel.ProductList.Add(cartItem);
                        }
                    }
                    OrderModel.PaymentChoice = PaymentChoice;
                    OrderModel.ShipmentChoice = ShipmentChoice;
                    OrderModel.ShippingAddress = LoggedInAs.Addresses.FirstOrDefault(a => a.ID == AddressID);
                    Order = _orderRepository.CreateOrder(OrderModel);
                    _productRepository.UpdateSaldo(OrderModel);

                    var productList = _cartItemRepository.CreateCartItem(OrderModel.ProductList);
                    _orderRepository.UpdateOrderProductList(Order, productList);

                    HttpContext.Session.SetString("cart", JsonSerializer.Serialize(new List<Guid>()));
                    LoggedInAs.ProductCart.Clear();

                    return Page();
                }
            }
            return RedirectToPage("/cart");
        }
    }
}
