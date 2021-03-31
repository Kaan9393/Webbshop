using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Enums;

namespace Kladbutiken.Pages.Customer
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public User LoggedInAs { get; set; }
        public Shipment Shipment { get; set; }
        public string Fraktsätt { get; set; }

        public OrderHistoryModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            if (userDetailsCookie != null)
            {
                LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);
            }
        }
    }
}
