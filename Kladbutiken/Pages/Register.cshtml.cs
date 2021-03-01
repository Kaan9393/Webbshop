using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public User LoggedInAs { get; set; }

        public RegisterModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public UserRegisterModel UserModel { get; set; } = new UserRegisterModel();

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userRepository.CreateUser(UserModel);
            return RedirectToPage("/Login");
        }
    }
}
