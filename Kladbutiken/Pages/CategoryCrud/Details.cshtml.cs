using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Kladbutiken.Pages.CategoryCrud
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Data.MainContext _context;
        private readonly IUserRepository _userRepository;
        public DetailsModel(DataAccess.Data.MainContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public User LoggedInAs { get; set; }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var user = _userRepository.GetUserByEmail(userDetailsCookie);
            LoggedInAs = user;
            LoggedInAs.EmailAddress = user.EmailAddress;

            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.ID == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
