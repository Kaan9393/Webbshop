using DataAccess.Entities;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        void CheckForAdmin();
        void CreateUser(UserRegisterModel model);
        User GetUserByEmail(string email);
        User LoginUser(UserLoginModel userLogin);

        void AddProductToCart(string email, Product product);
    }
}