using DataAccess.Entities;
using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface ICartItemRepository
    {
        void CreateCartItem(List<CartItemModel> productList, Order order);
    }
}