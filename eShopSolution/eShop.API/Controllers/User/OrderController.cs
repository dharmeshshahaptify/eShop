using eShop.API.Controllers.User;
using eShop.API.Helper.Interfaces;
using eShop.Entities;
using eShop.Models;
using eShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        IOrderService _orderService;

        public OrderController(IUserAccessor userAccessor, IOrderService orderService) : base(userAccessor)
        {
            _orderService = orderService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Get User Orders", "Retrieve User Orders")]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetUserOrders()
        {
            var orders = _orderService.GetUserOrders(CurrentUser.Id);
            return Ok(orders);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Get Specific Orders", "Get Specific Orders")]
        [HttpGet]
        public ActionResult<OrderModel> OrderDetails(string OrderId)
        {
            OrderModel Order = _orderService.GetOrderDetails(OrderId);
            return Ok(Order);
        }

    }
}
