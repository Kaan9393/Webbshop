using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}