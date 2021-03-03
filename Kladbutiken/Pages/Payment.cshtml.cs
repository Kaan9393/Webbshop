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
    public class PaymentModel : PageModel
    {
        public User LoggedInAs { get; set; }
        public Order Order { get; set; } = new();

        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentModel(IUserRepository userRepository, IOrderRepository orderRepository )
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }
        public void OnGet()
        {
            //var userDetailsCookie = Request.Cookies["UserDetails"];
            //if (userDetailsCookie != null)
            //{
            //    LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);
            //    var cart = HttpContext.Session.GetString("cart");
            //    if (cart != null)
            //    {
            //        LoggedInAs.ProductCart = JsonSerializer.Deserialize<List<Product>>(cart);
            //        Order.ProductList = LoggedInAs.ProductCart;
            //        _orderRepository.CreateOrder(Order);
            //    }
            //}
        }
    }
}
