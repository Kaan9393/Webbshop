using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Linq;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMainContext _context;

        public OrderRepository(IMainContext context)
        {
            _context = context;
        }
        
        public Order CreateOrder(OrderModel order)
        {
            var user = _context.Users.Single(u => u.ID == order.User.ID);

            Order newOrder = new()
            {
                ID = Guid.NewGuid(),
                Status = "Skickad",
                OrderDate = DateTime.Now,
            };
            user.Orders.Add(newOrder);
            var returnOrder =_context.Orders.Add(newOrder);
            _context.SaveChanges();
            return returnOrder.Entity;
        }
    }
}
