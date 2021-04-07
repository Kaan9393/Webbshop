using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public UserInfoModel CustomerInfo { get; set; } = new();

        public User LoggedInAs { get; set; }
        public int AddressNumber { get; set; }

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
