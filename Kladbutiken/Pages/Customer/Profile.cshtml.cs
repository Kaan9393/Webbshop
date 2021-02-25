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
    public class ProfileModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public ProfileModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [BindProperty]
        public UserInfoModel CustomerInfo { get; set; } = new UserInfoModel();

        //public User UserInfo { get; set; }
        public User LoggedInAs { get; set; }

        public void OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var user = _userRepository.GetUserByEmail(userDetailsCookie);
            LoggedInAs = user;

            if (user == null)
            {
                LoggedInAs = new User();
                LoggedInAs.EmailAddress = "Guest";
            }
            else
            {
                LoggedInAs.EmailAddress = user.EmailAddress;
            }
        }

        
    }
}
