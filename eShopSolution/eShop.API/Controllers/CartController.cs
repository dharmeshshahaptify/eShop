using eShop.Services.Interfaces;
using eShop.API.Helper.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Entities;
using Swashbuckle.AspNetCore.Annotations;
using eShop.API.Helper;

namespace eShop.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        ICartService _cartService;
        IUserAccessor _userAccessor;
        public Entities.User CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
            }
        }

        Guid CartId
        {
            get
            {
                Guid Id;
                string CId = Request.Cookies["CId"];
                if (string.IsNullOrEmpty(CId))
                {
                    Id = Guid.NewGuid();
                    //newly added
                    Response.Cookies.Append("CId", Id.ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(1) });
                }
                else
                {
                    Id = Guid.Parse(CId);
                }
                return Id;
            }
        }
        public CartController(ICartService cartService, IUserAccessor userAccessor)
        {
            _cartService = cartService;
            _userAccessor = userAccessor;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("AddtoCart", "Pass itemid, price, quantity")]
        //[CustomAuthorize(Roles = "User")]
        [HttpPost]
        public ActionResult<Cart> AddToCart(int ItemId, decimal UnitPrice, int Quantity)
        {
            int UserId = CurrentUser != null ? CurrentUser.Id : 0;

            if (ItemId > 0 && Quantity > 0)
            {
                Cart cart =  _cartService.AddItem(UserId, CartId, ItemId, UnitPrice, Quantity);
                return Ok(cart);
            }
            else
            {
                return Ok();
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public IActionResult DeleteItem(int Id)
        {
            int count = _cartService.DeleteItem(CartId, Id);
            return Ok(count);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Cart/UpdateQuantity/{Id}/{Quantity}")]
        [HttpPut]
        public IActionResult UpdateQuantity(int Id, int Quantity)
        {
            int count = _cartService.UpdateQuantity(CartId, Id, Quantity);
            return Ok(count);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetCartCount()
        {
            int count = _cartService.GetCartCount(CartId);
            return Ok(count);
        }







    }
}
