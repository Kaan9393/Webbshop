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
        public OrderModel Order { get; set; } = new();

        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public PaymentModel(IUserRepository userRepository, IOrderRepository orderRepository, IProductRepository productRepository )
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
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
                    //Order.ProductList = LoggedInAs.ProductCart;
                    Order.User = LoggedInAs;
                    foreach (var product in LoggedInAs.ProductCart)
                    {
                        if (Order.ProductList.Any(c => c.Product.ID == product.ID))
                        {
                            var cartItem = Order.ProductList.FirstOrDefault(c => c.Product.ID == product.ID);
                            cartItem.Quantity += 1;
                        }
                        else
                        {
                            var cartItem = new CartItemModel() { Product = product, Quantity = 1 };
                            Order.ProductList.Add(cartItem);
                        }
                    }
                    _orderRepository.CreateOrder(Order);
                }
            }
            
        }
    }
}
