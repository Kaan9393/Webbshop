using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task UpdateUser(UserInfoModel model, Guid userID);
        void CheckForAdmin();
        void CreateUser(UserRegisterModel model);
        User GetUserByEmail(string email);
        User LoginUser(UserLoginModel userLogin);
    }
}