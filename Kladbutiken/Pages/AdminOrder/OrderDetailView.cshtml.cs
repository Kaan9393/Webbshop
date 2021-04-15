using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages.AdminOrder
{
    public class OrderDetailViewModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public User LoggedInAs { get; set; }
        [BindProperty]
        public Order SelectedOrder { get; set; }

        [BindProperty(SupportsGet =true)]
        public Guid OrderId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OrderStatus { get; set; }
        [BindProperty]
        public double TotalPrice { get; set; }

        public OrderDetailViewModel(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }
        public IActionResult OnGet()
        {
            SelectedOrder = _orderRepository.GetOrderById(OrderId);
            foreach (var item in SelectedOrder.ProductList)
            {
                TotalPrice += item.Product.PriceWithDiscount;
            }

            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

            if (LoggedInAs.Role != "Admin")
            {
                return RedirectToPage("/index");
            }

            
            return Page();
        }
        public IActionResult OnPostProceed()
        {
            _orderRepository.UpdateOrderStatus(OrderId);
            return Redirect($"/AdminOrder/OrderDetailView?orderId={OrderId}");
        }
        public IActionResult OnPostCancel()
        {
            _orderRepository.CancelOrder(OrderId);
            return Redirect($"/AdminOrder/OrderDetailView?orderId={OrderId}");
        }
    }
}
