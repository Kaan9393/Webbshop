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
    public class EditAddressModel : PageModel
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;


        [BindProperty(SupportsGet = true)]
        public Guid ID { get; set; }

        public Address Address { get; set; } = new Address();
        public User LoggedInAs { get; set; }

        [BindProperty]
        public Address UpdatedAddress { get; set; } = new Address();

        public EditAddressModel(IAddressRepository addressRepository,IUserRepository userRepository )
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
        }

        public void OnGet()
        {
            Address=_addressRepository.GetAddressByID(ID);

            var userDetailsCookie = Request.Cookies["UserDetails"];
            if (userDetailsCookie != null)
            {
                LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);
            }
        }

        public IActionResult OnPost()
        {
            _addressRepository.UpdateAddress(UpdatedAddress, ID);
            return RedirectToPage("/Customer/Profile");
        }
    }
}
