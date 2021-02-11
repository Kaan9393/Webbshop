using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Linq;


namespace DataAccess.Repositories
{
    public static class UserRepository
    {
        public static void CreateUser(UserModel model)
        {
            using (var db = new MainContext())
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
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public static User LoginUser(UserLoginModel userLogin)
        {
            using (var db = new MainContext())
            {
                return db.Users.FirstOrDefault(u => u.EmailAddress == userLogin.UserName && u.Password == userLogin.Password);
            }
        }

        public static User GetUserByEmail(string email)
        {
            using (var db = new MainContext())
            {
                return db.Users.FirstOrDefault(u => u.EmailAddress == email);
            }
        }

        public static void CheckForAdmin()
        {
            using (var db = new MainContext())
            {
                var admin = db.Users.FirstOrDefault(u => u.Role == "Admin");

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
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }
    }
}
