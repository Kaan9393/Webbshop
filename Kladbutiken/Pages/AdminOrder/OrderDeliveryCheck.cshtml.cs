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
        private readonly IProductRepository _productRepository;
        private readonly IMainContext _context;

        public User LoggedInAs { get; set; }

        public OrderDeliveryCheckModel(IUserRepository userRepository, IProductRepository productRepository, IMainContext context)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _context = context;
        }


        public List<Order> AllOrders { get; set; }
        public string OrderStatus { get; set; }


        public List<Order> GetOrders()
        {
           
        }
        public void OnGet()
        {
        }
    }
}
