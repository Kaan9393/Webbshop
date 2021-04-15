using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kladbutiken.Pages.AdminOrder
{
    public class OrderDeliveryCheckModel : PageModel
    {

        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public User LoggedInAs { get; set; }
        public List<Order> AllOrders { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OrderStatus { get; set; }

        [BindProperty(SupportsGet =true)]
        public Guid OrderId { get; set; }

        public OrderDeliveryCheckModel(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult OnGet()
        {
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

            if (OrderStatus is null)
            {
                return NotFound();
            }

            AllOrders = _orderRepository.GetOrderByStatus(OrderStatus);

            return Page();
        }

        public IActionResult OnPostProceed()
        {
            _orderRepository.UpdateOrderStatus(OrderId);
            return Redirect($"/AdminOrder/OrderDeliveryCheck?orderstatus={OrderStatus}");
        }
        public IActionResult OnPostCancel()
        {
            _orderRepository.CancelOrder(OrderId);
            return Redirect($"/AdminOrder/OrderDeliveryCheck?orderstatus={OrderStatus}");
        }
    }
}
