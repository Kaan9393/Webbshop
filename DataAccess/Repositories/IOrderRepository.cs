using DataAccess.Entities;
using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(OrderModel order);
    }
}