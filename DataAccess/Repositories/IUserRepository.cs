using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(UserModel model);
    }
}