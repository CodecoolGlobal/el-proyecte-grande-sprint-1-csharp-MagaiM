﻿using System;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElProyecteGrandeSprint1.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeWithTokenAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _acceptedRole;

        public AuthorizeWithTokenAttribute(string acceptedRole)
        {
            _acceptedRole = acceptedRole.Split(",");
        }

        public AuthorizeWithTokenAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var dbContext = context.HttpContext
                .RequestServices
                .GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            var userService = context.HttpContext.RequestServices.GetService(typeof(UserService)) as UserService;
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null || dbContext.JWTTokens.FirstOrDefault(t => t.Token == token) == null)
            {
                // not logged in
                context.Result = new JsonResult(new {message = "Unauthorized"})
                    {StatusCode = StatusCodes.Status401Unauthorized};
                return;
            }

            var userName = DecodeJWT(token).Claims.ElementAt(1).Value;
            if (!CheckRoles(userService, userName))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) 
                    { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
        }

        private bool CheckRoles(UserService userService, string username)
        {
            if (_acceptedRole is null)
                return true;
            var searchedUser = userService.GetUserByName(username);
            var rolesList = searchedUser.Result.Roles.Select(role => role.Name).ToList();
            foreach (var role in _acceptedRole)
            {
                if (rolesList.Contains(role))
                    return true;
            }
            return false;
        }

        private JwtSecurityToken DecodeJWT(string JWT)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(JWT);
        }
    }
}