using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly IMainContext _context;

        public CartItemRepository(IMainContext context)
        {
            _context = context;
        }

        public List<CartItem> CreateCartItem(List<CartItemModel> productList)
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
            
            _context.CartItem.AddRange(cartItems);
            _context.SaveChanges();
            return cartItems;
        }
    }
}
