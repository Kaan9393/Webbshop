using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages.Customer
{
    public class EditAddressModel : PageModel
    {
        private readonly IAddressRepository _addressRepository;

        [BindProperty(SupportsGet = true)]
        public Guid ID { get; set; }

        public Address Address { get; set; } = new Address();

        [BindProperty]
        public Address UpdatedAddress { get; set; } = new Address();

        public EditAddressModel(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public void OnGet()
        {
            Address=_addressRepository.GetAddressByID(ID);
        }

        public IActionResult OnPost()
        {
            _addressRepository.UpdateAddress(UpdatedAddress, ID);
            return RedirectToPage("/Customer/Profile");
        }
    }
}
