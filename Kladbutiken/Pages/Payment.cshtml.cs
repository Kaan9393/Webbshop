using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class PaymentModel : PageModel
    {
        public User LoggedInAs { get; set; }

        public OrderModel OrderModel { get; set; } = new();

        public Order Order { get; set; }

        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public PaymentModel(IUserRepository userRepository, IOrderRepository orderRepository, IProductRepository productRepository, ICartItemRepository cartItemRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _cartItemRepository = cartItemRepository;
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
                    OrderModel.User = LoggedInAs;
                    foreach (var product in LoggedInAs.ProductCart)
                    {
                        if (OrderModel.ProductList.Any(c => c.Product.ID == product.ID))
                        {
                            var cartItem = OrderModel.ProductList.FirstOrDefault(c => c.Product.ID == product.ID);
                            cartItem.Quantity += 1;
                        }
                        else
                        {
                            var cartItem = new CartItemModel { Product = product, Quantity = 1 };
                            OrderModel.ProductList.Add(cartItem);
                        }
                    }
                    Order = _orderRepository.CreateOrder(OrderModel);
                    _productRepository.UpdateSaldo(OrderModel);

                    _cartItemRepository.CreateCartItem(OrderModel.ProductList, Order);
                    
                    HttpContext.Session.SetString("cart", JsonSerializer.Serialize(new List<Guid>()));
                    LoggedInAs.ProductCart.Clear();

                    return Page();
                }
            }
            return RedirectToPage("/cart");
        }
    }
}
