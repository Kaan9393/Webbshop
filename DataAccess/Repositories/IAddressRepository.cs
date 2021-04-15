using DataAccess.Entities;
using DataAccess.Models;
using System;

namespace DataAccess.Repositories
{
    public interface IAddressRepository
    {
        Address GetAddressByID(Guid AddressID);
        void UpdateAddress(Address updatedAddress, Guid AddressID);
        void DeleteAddress(Guid AddressID);
        void AddAddress(AddressModel model);
    }
}