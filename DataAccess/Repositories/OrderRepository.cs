using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMainContext _mainContext;

        public OrderRepository(IMainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public void CreateOrder(Order order)
        {
            _mainContext.Orders.Attach(order).State = EntityState.Modified; 
            _mainContext.SaveChanges();
        }
    }
}
