using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace api.pet.Infrastructure.ActionResult
{
    public class ResultWithChallenge : IHttpActionResult
    {
        private readonly IHttpActionResult _next;
        private readonly string _realm;

        public ResultWithChallenge(IHttpActionResult next, string realm)
        {
            this._next = next;
            this._realm = realm;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var res = await _next.ExecuteAsync(cancellationToken);
            if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                res.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", this._realm));
            }

            //Custom header
            res.Headers.Add("AA", DateTime.UtcNow.ToString());
            return res;
        }
    }
}