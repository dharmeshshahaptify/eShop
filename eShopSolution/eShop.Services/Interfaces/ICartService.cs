using eShop.Entities;
using eShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Services.Interfaces
{
    public interface ICartService
    {
        int GetCartCount(Guid cartId);
        CartModel GetCartDetails(Guid cartId);
        Cart AddItem(int UserId, Guid CartId, int ItemId, decimal UnitPrice, int Quantity);
        int DeleteItem(Guid cartId, int ItemId);
        int UpdateQuantity(Guid CartId, int Id, int Quantity);
        int UpdateCart(Guid CartId, int UserId);
    }
}
