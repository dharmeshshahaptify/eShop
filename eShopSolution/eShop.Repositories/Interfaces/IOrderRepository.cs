﻿using eShop.Entities;
using eShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        OrderModel GetOrderDetails(string id);
        IEnumerable<Order> GetUserOrders(int UserId);
        PagingListModel<OrderModel> GetOrderList(int page, int pageSize);
    }
}
