using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SerkoGSTCalculatorApi.Business;
using SerkoGSTCalculatorApi.Web.Helpers;

namespace SerkoGSTCalculatorApi.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IConfiguration config;
        private readonly ICore core;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration config,
            ICore core)
            : base(options, logger, encoder, clock)
        {
            this.config = config;
            this.core = core;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Keys.ApiHeader.Authorization))
                return AuthenticateResult.Fail("Missing Authorization Header");

            string timestamp;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[Keys.ApiHeader.Authorization]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(Keys.Common.Delimiter);
                var apiKey = credentials[0];
                timestamp = credentials[1];
                if (apiKey != config.GetSection("AppSettings")["ApiKey"])
                {
                    throw new Exception("Incorrect Api Key");
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, timestamp),
                new Claim(ClaimTypes.Name, timestamp),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
