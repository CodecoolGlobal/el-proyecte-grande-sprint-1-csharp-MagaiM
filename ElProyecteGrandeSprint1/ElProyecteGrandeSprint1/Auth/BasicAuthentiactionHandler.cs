using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ElProyecteGrandeSprint1.Auth
{
    public class BasicAuthentiactionHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BasicAuthentiactionHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ApplicationDbContext applicationDbContext)
            : base(options, logger, encoder, clock)
        {
            _applicationDbContext = applicationDbContext;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] or [AuthorizeWithTokenAttribute] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null || endpoint?.Metadata?.GetMetadata<AuthorizeWithTokenAttribute>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            return AuthenticateResult.NoResult();
            // implement your authentication logic here
            //var authorizationHeader = Request.Headers.Authorization.ToString();

            //var userInfoEncoded = new string(authorizationHeader.Skip(6).ToArray()); // Remove the "Basic " start of the header value

            //var userInfoDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(userInfoEncoded));

            //var userName = userInfoDecoded.Split(":")[0];
            //var password = userInfoDecoded.Split(":")[1];
            //var user = _applicationDbContext.Users.Include(u => u.Roles).FirstOrDefault(user => user.UserName == userName && user.Password.ValidatePassword(password));
            //// var user = _applicationDbContext.Users.FirstOrDefault(user => user.UserName == userName && user.Password == password);

            //if (user == null)
            //    return AuthenticateResult.Fail("Wrong username and or password!");

            //var claims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name, user.UserName)
            //};

            //// add user roles as claims here
            //foreach (var role in user.Roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.Name));
            //}


            //var identity = new ClaimsIdentity(claims, Scheme.Name);
            //var principal = new ClaimsPrincipal(identity);
            //var ticket = new AuthenticationTicket(principal, Scheme.Name);

            //return AuthenticateResult.Success(ticket);
        }
    }
}
