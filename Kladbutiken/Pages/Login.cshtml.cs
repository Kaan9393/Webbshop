using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Entities;

namespace Kladbutiken.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public User LoggedInAs { get; set; }

        public LoginModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public UserLoginModel UserLoginModel { get; set; }

        public IActionResult OnPost()
        {
            var user = _userRepository.LoginUser(UserLoginModel);

            if (user is null)
            {
                return RedirectToPage("/login");
            }

            Response.Cookies.Append("UserDetails", user.EmailAddress);

            return RedirectToPage(user.Role == "Admin" ? "/admin" : "/index");
        }
    }
}
