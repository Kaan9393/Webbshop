using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Kladbutiken.Pages.CategoryCrud
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.Data.MainContext _context;
        private readonly IUserRepository _userRepository;
        public EditModel(DataAccess.Data.MainContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public User LoggedInAs { get; set; }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
