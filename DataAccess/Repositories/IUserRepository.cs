using DataAccess.Entities;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        void CheckForAdmin();
        void CreateUser(UserModel model);
        User GetUserByEmail(string email);
        User LoginUser(UserLoginModel userLogin);
    }
}