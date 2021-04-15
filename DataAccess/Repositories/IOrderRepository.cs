using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Order CreateOrder(OrderModel order);
        void UpdateOrderProductList(Order order,List<CartItem> productList);
        List<Order> GetOrderByStatus(string status);
        void UpdateOrderStatus(Guid Id);
        void CancelOrder(Guid orderId);
        Order GetOrderById(Guid orderId);
    }
}