using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IMainContext _context;
        private readonly IUserRepository _userRepository;
        private readonly MainContext _main;

        public AddressRepository(IMainContext context, IUserRepository userRepository, MainContext main)
        {
            _context = context;
            _userRepository = userRepository;
            _main = main;
        }
        public Address GetAddressByeID(Guid AddressID)
        {
            return _context.Addresses.Single(a => a.ID == AddressID);
        }
        public void UpdateAddress(Address updatedAddress, Guid AddressID)
        {
            var addressToUpdate = _context.Addresses.Single(a=>a.ID==AddressID);

            addressToUpdate.City = updatedAddress.City;
            addressToUpdate.Country = updatedAddress.Country;
            addressToUpdate.PostalCode = updatedAddress.PostalCode;
            addressToUpdate.State = updatedAddress.State;
            addressToUpdate.Street = updatedAddress.Street;
            _context.SaveChanges();
        }
        public void DeleteAddress(Guid AddressID)
        {
            var addressToDelete=_context.Addresses.Single(a => a.ID == AddressID);
            _context.Addresses.Remove(addressToDelete);
            _context.SaveChanges();
        }
        public void AddAddress(AddressModel model, User userToAddToAddress)
        {

            var address = new Address
            {
                City = model.City,
                Country = model.Country,
                PostalCode = model.PostalCode,
                State = model.State,
                Street = model.Street,
                User = userToAddToAddress
            };
            //_context.Addresses.Add(address);
            _main.Addresses.Add(address);
            userToAddToAddress.Addresses.Add(address);
            _main.SaveChanges();
            //_context.SaveChanges();

        }

    }
}
