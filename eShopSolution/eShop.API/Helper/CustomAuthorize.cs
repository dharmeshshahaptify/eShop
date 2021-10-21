using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eShop.API.Helper
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Authentication
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //TO DO:Check Permission 


                //Authorization
                if (!context.HttpContext.User.IsInRole(Roles))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    context.Result =   new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Invalid Token"
                        },
                    };
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Login";
                context.Result = new JsonResult("Please Login")
                {
                    Value = new
                    {
                        Status = "Error",
                        Message = "Please Login"
                    },
                };

            }
        }
    }
}
