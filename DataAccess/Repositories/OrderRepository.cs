using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
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
        private readonly MainContext _context;

        public OrderRepository(IMainContext mainContext, MainContext context)
        {
            _mainContext = mainContext;
            _context = context;
        }
        public List<CartItem> CreateCartItem(List<CartItemModel> productList)
        {
            List <CartItem> list= new();

            foreach (var product in productList)
            {
                CartItem cartItem = new()
                {
                    ID = Guid.NewGuid(),
                    Product=product.Product,
                    Quantity=product.Quantity
                };
                list.Add(cartItem);
            }
            //_context.AddRange();
            //_context.Entry()
            _mainContext.AddRange(list);
            _mainContext.SaveChanges();
            return list;
           
        }
        public void CreateOrder(OrderModel order)
        {
            var user = _context.Users.Single(u => u.ID == order.User.ID);
            //_mainContext.Orders.Attach(order).State = EntityState.Modified; 
            //int i = 1;
            //foreach (var product in productList)
            //{
            //    if (productList.Any(p=>p.ID == product.ID))
            //    {
            //        order.ProductList.Add(productList.FirstOrDefault(c => c.ID == product.ID));

            //        i += 1;
            //    }
            //    product.StockBalance = i;
            //}

            Order newOrder = new()
            {
                ProductList=CreateCartItem(order.ProductList),
                OrderDate=DateTime.Now,
            };
            user.Orders.Add(newOrder);
            _context.SaveChanges();
        }
    }
}
