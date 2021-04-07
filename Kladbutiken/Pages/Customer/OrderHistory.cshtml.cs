using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Enums;
using Kladbutiken.Utils;
using Microsoft.AspNetCore.Http;

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
        public async Task OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");
            if (userDetailsCookie != null)
            {
                LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);
            }
        }
    }
}
