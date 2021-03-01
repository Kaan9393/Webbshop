using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages.Customer
{
    public class AddAddressModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;


        public AddAddressModel(IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }
        public User LoggedInAs { get; set; }

        [BindProperty]
        public AddressModel Model { get; set; } = new AddressModel();

        public void OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);
        }
        public IActionResult OnPost()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

            _addressRepository.AddAddress(Model, LoggedInAs);
            return RedirectToPage("/Customer/Profile");
        }
    }
}
