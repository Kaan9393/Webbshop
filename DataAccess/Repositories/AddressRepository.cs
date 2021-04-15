using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Linq;

namespace DataAccess.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IMainContext _context;

        public AddressRepository(IMainContext context)
        {
            _context = context;
        }

        public Address GetAddressByID(Guid AddressID)
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
            var addressToDelete = _context.Addresses.Single(a => a.ID == AddressID);
            _context.Addresses.Remove(addressToDelete);
            _context.SaveChanges();
        }

        public void AddAddress(AddressModel model)
        {
            var userToAddToAddress = _context.Users.Single(u => u.ID == model.UserId);

            var address = new Address
            {
                ID = Guid.NewGuid(),
                City = model.City,
                Country = model.Country,
                PostalCode = model.PostalCode,
                State = model.State,
                Street = model.Street,
                User = userToAddToAddress
            };

            userToAddToAddress.Addresses.Add(address);
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }
    }
}
