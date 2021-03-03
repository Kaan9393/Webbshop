using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Kladbutiken.Pages
{
    public class EditModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        [BindProperty]
        public UserInfoModel CustomerInfo { get; set; } = new();

        [BindProperty]
        public ChangePasswordModel PasswordModel { get; set; } = new();

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
            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);
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

        public IActionResult OnPostChangePassword()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            if (userDetailsCookie is null)
            {
                Forbid();
            }

            LoggedInAs = _userRepository.GetUserByEmail(userDetailsCookie);

            var isPasswordModelValid = true;
            var modelStateKeys = ModelState.FindKeysWithPrefix("PasswordModel");
            foreach (var key in modelStateKeys)
            {
                if (key.Key.Contains("CurrentPassword"))
                {
                    if (PasswordModel.CurrentPassword != LoggedInAs.Password)
                    {
                        key.Value.ValidationState = ModelValidationState.Invalid;
                        key.Value.Errors.Add("Fel l�senord!");
                    }
                }
                if (key.Value.ValidationState == ModelValidationState.Invalid)
                {
                    isPasswordModelValid = false;
                }
            }

            if (isPasswordModelValid)
            {
                _userRepository.UpdatePassword(ID, PasswordModel.NewPassword);
                return RedirectToPage("/Customer/Profile");
            }
            else
            {
                return Page();
            }
        }
    }
}