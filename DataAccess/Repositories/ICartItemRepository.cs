using DataAccess.Entities;
using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface ICartItemRepository
    {
        List<CartItem> CreateCartItem(List<CartItemModel> productList);
    }
}