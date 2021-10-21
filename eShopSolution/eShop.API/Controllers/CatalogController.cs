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
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;


        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;   
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation("GetProducts", "Retrieve Product List")]
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            try
            {

                var data = _catalogService.GetItems();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


    }
}
