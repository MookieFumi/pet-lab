using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using api.pet.Infrastructure.ActionResult;

namespace api.pet.Infrastructure.Attributes
{
    public class VeemerBasicAuthentication : Attribute, IAuthenticationFilter
    {
        //https://lbadri.wordpress.com/2014/02/13/basic-authentication-with-asp-net-web-api-using-authentication-filter/
        private readonly string _realm;
        public bool AllowMultiple { get { return false; } }

        private string GetUserName()
        {
            return Utilities.GetAppUserName();
        }

        private string GetPassword()
        {
            return Utilities.GetAppPassword();
        }

        public VeemerBasicAuthentication(string realm)
        {
            _realm = "realm=" + realm;
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var req = context.Request;
            if (req.Headers.Authorization != null &&
                req.Headers.Authorization.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                var credentials = encoding.GetString(Convert.FromBase64String(req.Headers.Authorization.Parameter));
                var parts = credentials.Split(':');
                var user = parts[0].Trim();
                var password = parts[1].Trim();

                //TODO: User/ password
                if (user.Equals(GetUserName()) && password.Equals(GetPassword()))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, "Miguel Angel Martín Hrdez")
                    };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    var principal = new ClaimsPrincipal(new[] { identity });
                    context.Principal = principal;
                }
                else
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                }
            }
            else
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result, _realm);
            return Task.FromResult(0);
        }
    }
}