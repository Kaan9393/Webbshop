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

        public EditAddressModel(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        [BindProperty(SupportsGet = true)]
        public Guid ID { get; set; }
        public Address Address { get; set; } = new Address();
        [BindProperty]
        public Address UpdatedAddress { get; set; } = new Address();
        public void OnGet()
        {
            Address=_addressRepository.GetAddressByeID(ID);
        }
        public void OnPost()
        {
            //Metod i repository som våldtar Murcus i sjärten
            _addressRepository.UpdateAddress(UpdatedAddress, ID);
        }
    }
}
