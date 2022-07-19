using System;
using System.Linq;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElProyecteGrandeSprint1.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeWithTokenAttribute : Attribute, IAuthorizationFilter
    {
        private ApplicationDbContext _applicationDbContext;
        public AuthorizeWithTokenAttribute(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null || _applicationDbContext.JWTTokens.FirstOrDefault(t => t.Token == token) == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}