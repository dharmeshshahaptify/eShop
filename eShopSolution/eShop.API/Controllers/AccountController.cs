using eShop.API.Model;
using eShop.Entities;
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
    public class AccountController : ControllerBase
    {
        IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("AuthenticateUser", "Authenticate User")]
        [HttpPost]
        public async Task<ActionResult<Entities.User>> AuthenticateUser(LoginModel model)
        {
            var data = await _authService.AuthenticateUser(model.Email,model.Password);
            if (data != null)
                return Ok(data);
            else
                return NotFound("user not found");
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("SignUp", "Create User")]
        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel model)
        {
           
                Entities.User user = new Entities.User
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                bool result = await _authService.CreateUser(user, model.Password);
                if (result)
                    return StatusCode(StatusCodes.Status201Created);
                else
                    return StatusCode(StatusCodes.Status400BadRequest);

          
                
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation("Logout", "LogUser")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
             var result = await _authService.SignOut();
            if (result)
                return StatusCode(StatusCodes.Status204NoContent);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
}
