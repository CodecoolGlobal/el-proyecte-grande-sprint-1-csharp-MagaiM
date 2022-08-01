using System.Text.Encodings.Web;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ElProyecteGrandeSprint1.Auth
{
    public class BasicAuthentiactionHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthentiactionHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ApplicationDbContext applicationDbContext)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null || endpoint?.Metadata?.GetMetadata<AuthorizeWithTokenAttribute>() != null)
                return Task.FromResult(AuthenticateResult.NoResult());

            if (!Request.Headers.ContainsKey("authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

            return Task.FromResult(AuthenticateResult.NoResult());
        }
    }
}
