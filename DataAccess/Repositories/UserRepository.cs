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
                User user = new User();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.EmailAddress = model.Email;
                user.Password = model.Password;
                user.RegisterDate = DateTime.Now;
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
    }
}
