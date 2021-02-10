using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserModel UserModel { get; set; } = new UserModel();

        public void OnPost()
        {
            UserRepository.CreateUser(UserModel);
        }
    }
}
