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
    public class CustomerModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public CustomerModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [BindProperty]
        public UserInfoModel CustomerInfo { get; set; } = new UserInfoModel();

        public User UserInfo { get; set; }
        public string LoggedInAs { get; set; }

        public void OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var user = _userRepository.GetUserByEmail(userDetailsCookie);
            UserInfo = user;
            if (user == null)
            {
                LoggedInAs = "Guest";
            }
            else
            {
                LoggedInAs = user.EmailAddress;
            }
        }

        public void OnPost()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var user = _userRepository.GetUserByEmail(userDetailsCookie);
            UserInfo = user;
            if (user == null)
            {
                LoggedInAs = "Guest";
            }
            else
            {
                LoggedInAs = user.EmailAddress;
            }
            _userRepository.AddUserInfo(CustomerInfo);
        }
    }
}
