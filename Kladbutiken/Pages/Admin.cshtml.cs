using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class AdminModel : PageModel
    {

        public string LoggedInAs { get; set; }

        public IActionResult OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];

            if (userDetailsCookie == null)
            {
                return RedirectToPage("/login");
            }

            var user = UserRepository.GetUserByEmail(userDetailsCookie);

            if (user.Role != "Admin")
            {
                return RedirectToPage("/index");
            }

            LoggedInAs = user.EmailAddress;

            return Page();
        }

        
        public IActionResult OnPostLogout()
        {
            Response.Cookies.Delete("UserDetails");

            return RedirectToPage("/index");
        }
    }
}
