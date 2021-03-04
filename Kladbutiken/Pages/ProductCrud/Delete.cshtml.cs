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

namespace Kladbutiken.Pages.ProductCrud
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Data.MainContext _context;
        private readonly IUserRepository _userRepository;

        public DeleteModel(DataAccess.Data.MainContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public User LoggedInAs { get; set; }

        [BindProperty]
        public Product Product { get; set; }

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

            Product = await _context.Products.FirstOrDefaultAsync(m => m.ID == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FindAsync(id);

            if (Product != null)
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
