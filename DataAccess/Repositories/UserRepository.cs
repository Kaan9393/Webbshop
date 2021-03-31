using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMainContext _context;

        public UserRepository(IMainContext context)
        {
            _context = context;
        }

        public void CreateUser(UserRegisterModel model)
        {
            User user = new User
            {
                Role = "Customer",
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.Email,
                Password = model.Password,
                RegisterDate = DateTime.Now,
                
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdatePassword(Guid userId, string newPassword)
        {
            var user = _context.Users.Single(u => u.ID == userId);
            user.Password = newPassword;
            _context.SaveChanges();
        }

        public void UpdateUser(UserInfoModel model, Guid userID)
        {
            var userToUpdate = _context.Users.Single(u => u.ID == userID);
            userToUpdate.FirstName = model.FirstName;
            userToUpdate.LastName = model.LastName;
            userToUpdate.EmailAddress = model.Email;
            userToUpdate.PhoneNumber = model.PhoneNumber;

            _context.SaveChanges();
        }
        public void DeleteUser(Guid userID)
        {
            var userToDelete= _context.Users.Include(u=>u.Addresses).Include(u => u.ProductCart).Include(u => u.Orders).ThenInclude(u => u.ProductList).Single(u => u.ID == userID);
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }

        public User LoginUser(UserLoginModel userLogin)
        {
            return _context.Users.FirstOrDefault(u => u.EmailAddress == userLogin.UserName && u.Password == userLogin.Password);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Include(x=>x.Addresses).Include(u => u.Orders).ThenInclude(o => o.ProductList).ThenInclude(c => c.Product).FirstOrDefault(u => u.EmailAddress == email);
        }

        public void CheckForAdmin()
        {
            var admin = _context.Users.FirstOrDefault(u => u.Role == "Admin");

            if (admin is null)
            {
                var user = new User
                {
                    Role = "Admin",
                    FirstName = "Admin",
                    LastName = "Adminsson",
                    EmailAddress = "admin@emptyhanger.net",
                    Password = "adminpassword",
                    RegisterDate = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }

    }
}
