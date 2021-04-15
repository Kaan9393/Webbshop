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

namespace Kladbutiken.Pages.Customer
{
    public class AddAddressModel : PageModel
    {
        private readonly IAddressRepository _addressRepository;

        public AddAddressModel(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public User LoggedInAs { get; set; }

        [BindProperty]
        public AddressModel Model { get; set; } = new();

        public async Task OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");
            if (userDetailsCookie == null)
            {
                RedirectToPage("/login");
            }

            LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);
        }
        public IActionResult OnPost()
        {
            _addressRepository.AddAddress(Model);

            return RedirectToPage("/Customer/Profile");
        }
    }
}
