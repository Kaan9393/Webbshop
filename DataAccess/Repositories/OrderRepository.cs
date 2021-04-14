using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMainContext _context;

        public OrderRepository(IMainContext context)
        {
            _context = context;
        }

        public List<Order> GetOrderByStatus(string status)
        {
            return _context.Orders.Include(o=>o.User).Where(o => o.Status.Equals(status)).ToList();
        }
        
        public Order CreateOrder(OrderModel order)
        {
            var user = _context.Users.Single(u => u.ID == order.User.ID);

            Order newOrder = new()
            {
                ID = Guid.NewGuid(),
                Status = "Mottagen",
                OrderDate = DateTime.Now,
                ShipmentChoice = order.ShipmentChoice,
                PaymentChoice = order.PaymentChoice,
                Address = $"{order.ShippingAddress.Street}, {order.ShippingAddress.PostalCode}, {order.ShippingAddress.City}"
            };
            newOrder.OrderNumber = $"{newOrder.ID}{newOrder.OrderDate}".GetHashCode().ToString();

            if (newOrder.OrderNumber.First()=='-')
            {
                newOrder.OrderNumber= newOrder.OrderNumber.Remove(0,1);

            }

            user.Orders.Add(newOrder);
            var returnOrder =_context.Orders.Add(newOrder);
            _context.SaveChanges();

            return returnOrder.Entity;
        }

        public void UpdateOrderProductList(Order order, List<CartItem> productList)
        {
            order.ProductList = productList;
            _context.Orders.Attach(order).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void UpdateOrderStatus(Guid orderId)
        {
            var orderToUpdate = _context.Orders.FirstOrDefault(o => o.ID == orderId);
            switch (orderToUpdate.Status)
            {
                case "Mottagen":
                    orderToUpdate.Status = "Behandlas";
                    break;
                case "Behandlas":
                    orderToUpdate.Status = "Skickad";
                    break;
                default:
                    break;
            }
            _context.SaveChanges();
        }

        public void CancelOrder(Guid orderId)
        {
            var orderToCancel = _context.Orders.FirstOrDefault(o => o.ID == orderId);
            orderToCancel.Status = "Avbruten";
            _context.SaveChanges();
        }
        public Order GetOrderById(Guid orderId)
        {
            return _context.Orders.Include(o=>o.User).Include(o=>o.ProductList).ThenInclude(p=>p.Product).FirstOrDefault(o => o.ID==orderId);
        }
    }
} 
