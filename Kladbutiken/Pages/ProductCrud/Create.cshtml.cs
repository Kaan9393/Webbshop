using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Data;
using DataAccess.Repositories;
using DataAccess.Entities;
using DataAccess.Models;

namespace Kladbutiken.Pages.ProductCrud
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _productrepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public CreateModel(IProductRepository productrepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _productrepository = productrepository;
            _categoryRepository= categoryRepository;
            _userRepository = userRepository;
        }

        public User LoggedInAs { get; set; }


        public List<SelectListItem> Categories { get; set; } = new();

        [BindProperty]
        public ProductAddModel Product { get; set; }

        [BindProperty]
        public Guid CategoryId { get; set; }


        public IActionResult OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var user = _userRepository.GetUserByEmail(userDetailsCookie);
            LoggedInAs = user;
            LoggedInAs.EmailAddress = user.EmailAddress;

            var categories =_productrepository.GetAllCategorys().ToList();  
            foreach (var category in categories)
            {
                Categories.Add(new SelectListItem { Value=category.ID.ToString(), Text=category.TypeName });
            }
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productrepository.AddProduct(Product,CategoryId);

            return RedirectToPage("./Index");
        }
    }
}
