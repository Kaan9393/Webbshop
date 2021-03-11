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
    public class LogOutModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public LogOutModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User LoggedInAs { get; set; }
        public IActionResult OnGet()
        {
            Response.Cookies.Delete("UserDetails");

            return RedirectToPage("/index");
        }
    }
}
