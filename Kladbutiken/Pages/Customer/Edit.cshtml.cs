using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class EditModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        [BindProperty]
        public UserInfoModel CustomerInfo { get; set; } = new UserInfoModel();

        [BindProperty]
        public Guid ID { get; set; }

        public User LoggedInAs { get; set; }

        public EditModel(IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        public void OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var user = _userRepository.GetUserByEmail(userDetailsCookie);
            LoggedInAs = user;
            if (user == null)
            {
                LoggedInAs.EmailAddress = "Guest";
            }
            else
            {
                LoggedInAs.EmailAddress = user.EmailAddress;
            }
        }

        public IActionResult OnPost()
        {
            _addressRepository.DeleteAddress(ID);

            return RedirectToPage("/Customer/Profile");
        }

        public IActionResult OnPostPersonupdate()
        {
            _userRepository.UpdateUser(CustomerInfo, ID);
            return RedirectToPage("/Customer/Profile");
        }
        public IActionResult OnPostDeleteUser()
        {
            Response.Cookies.Delete("UserDetails");
            _userRepository.DeleteUser(ID);
            return RedirectToPage("/Index");
        }
    }
}
