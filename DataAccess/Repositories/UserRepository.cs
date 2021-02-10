using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;


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
    }
}
