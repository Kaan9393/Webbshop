using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class ContactUsModel : PageModel
    {
        public User LoggedInAs { get; set; }

        public void OnGet()
        {
        }
    }
}
