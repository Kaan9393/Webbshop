using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class PaymentModel : PageModel
    {
        public User LoggedInAs { get; set; }

        private readonly IUserRepository _userRepository;

        public PaymentModel(IUserRepository userRepository)
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
