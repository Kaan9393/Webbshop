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
        private readonly MainContext _context;
        private readonly IMainContext _iContext;

        public UserRepository(MainContext context, IMainContext IContext)
        {
            _context = context;
            _iContext = IContext;
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

        public Task<int> UpdateUser(UserInfoModel model, Guid userID)
        {
            var userToUpdate = _iContext.Users.Single(u => u.ID == userID);
            /*var userToUpdate = */
            userToUpdate.FirstName = model.FirstName;
            userToUpdate.LastName = model.LastName;
            userToUpdate.EmailAddress = model.Email;
            userToUpdate.PhoneNumber = model.PhoneNumber;

            //Instantiera  listan
            userToUpdate.Addresses = new List<Address>();
            //Koppla address till user
            model.Address.User = userToUpdate;
            //Om Adressen inte finns skapa en ny
            if (!_iContext.Users.Any(a=>a.Addresses.Any(a=>a.Street==model.Address.Street)) /*Street==model.Address.Street)*/)
            {
                _iContext.Addresses.Add(model.Address);
                //_iContext.SaveChanges();
                userToUpdate.Addresses.Add(model.Address);
            }
            else
            {

            }
            return _iContext.SaveChangesAsync(new CancellationToken());

        }

        public User LoginUser(UserLoginModel userLogin)
        {
            return _context.Users.FirstOrDefault(u => u.EmailAddress == userLogin.UserName && u.Password == userLogin.Password);
        }

        public User GetUserByEmail(string? email)
        {
            return _context.Users.Include(x=>x.Addresses).FirstOrDefault(u => u.EmailAddress == email);
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

        public void AddProductToCart(string email, Product product)
        {
            User user = GetUserByEmail(email);
            user.ProductCart.Add(product);
            _context.SaveChanges();
        }
    }
}
