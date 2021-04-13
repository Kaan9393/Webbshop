using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using DataAccess.Utils;

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
            var salt = PasswordHasher.SaltGenerator();

            User user = new User
            {
                Role = "Customer",
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.Email,
                Salt = salt,
                Password = PasswordHasher.HashPassword(model.Password, salt),
                RegisterDate = DateTime.Now
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool ComparePassword(Guid userId, string passwordToCompare)
        {
            var user = _context.Users.Single(u => u.ID == userId);
            var hashedPasswordToCompare = PasswordHasher.HashPassword(passwordToCompare, user.Salt);

            return user.Password.Equals(hashedPasswordToCompare);
        }

        public void UpdatePassword(Guid userId, string newPassword)
        {
            var salt = PasswordHasher.SaltGenerator();
            var user = _context.Users.Single(u => u.ID == userId);
            user.Salt = salt;
            user.Password = PasswordHasher.HashPassword(newPassword, salt);
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
            var user = _context.Users.FirstOrDefault(u => u.EmailAddress == userLogin.UserName);

            if (user == null) return null;

            var loginPassword = PasswordHasher.HashPassword(userLogin.Password, user.Salt);

            return loginPassword.Equals(user.Password) ? user : null;
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
                var salt = PasswordHasher.SaltGenerator();

                var user = new User
                {
                    Role = "Admin",
                    FirstName = "Admin",
                    LastName = "Adminsson",
                    EmailAddress = "admin@emptyhanger.net",
                    Salt = salt,
                    Password = PasswordHasher.HashPassword("adminpassword", salt),
                    RegisterDate = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }
        
    }
}
