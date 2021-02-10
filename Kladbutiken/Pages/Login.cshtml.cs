using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Kladbutiken.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserLoginModel UserLoginModel { get; set; }

        public IActionResult OnPost()
        {
            var user = UserRepository.LoginUser(UserLoginModel);
            if (user == null)
            {
                return RedirectToPage("/login");
            }
            else
            {
                Response.Cookies.Append("UserDetails", user.EmailAddress);
                return RedirectToPage("/admin");
            }
        }
    }
}
