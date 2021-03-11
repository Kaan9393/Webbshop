using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly IMainContext _context;

        public CartItemRepository(IMainContext context)
        {
            _context = context;
        }

        public void CreateCartItem(List<CartItemModel> productList, Order order)
        {
            List<CartItem> cartItems = new();

            foreach (var product in productList)
            {
                CartItem cartItem = new CartItem
                {
                    ID = Guid.NewGuid(),
                    Product = product.Product,
                    Quantity = product.Quantity
                };
                cartItems.Add(cartItem);
            }

            order.ProductList = cartItems;
            _context.CartItem.AddRange(cartItems);
            _context.SaveChanges();
        }
    }
}
