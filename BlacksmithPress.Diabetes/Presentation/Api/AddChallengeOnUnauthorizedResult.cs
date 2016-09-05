using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlacksmithPress.Diabetes.Cloud
{
    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult result)
        {
            Challenge = challenge;
            InnerResult = result;
        }

        public IHttpActionResult InnerResult { get; set; }

        public AuthenticationHeaderValue Challenge { get; set; }
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await InnerResult.ExecuteAsync(cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (response.Headers.WwwAuthenticate.All(h => h.Scheme != Challenge.Scheme))
                    response.Headers.WwwAuthenticate.Add(Challenge);
            }
            return response;
        }
    }
}