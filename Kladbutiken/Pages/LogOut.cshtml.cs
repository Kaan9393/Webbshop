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
        public IActionResult OnGet()
        {
            Response.Cookies.Delete("UserDetails");
            HttpContext.Session.Remove("cart");

            return RedirectToPage("/index");
        }
    }
}
