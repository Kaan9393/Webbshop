using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Linq;


namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainContext _context;

        public UserRepository(MainContext context)
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
                RegisterDate = DateTime.Now
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User LoginUser(UserLoginModel userLogin)
        {
            return _context.Users.FirstOrDefault(u => u.EmailAddress == userLogin.UserName && u.Password == userLogin.Password);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.EmailAddress == email);
        }

        public void CheckForAdmin()
        {
            var admin = _context.Users.FirstOrDefault(u => u.Role == "Admin");

            if (admin is null)
            {
                User user = new User
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
