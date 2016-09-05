using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlacksmithPress.Diabetes.Cloud
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string message, HttpRequestMessage request)
        {
            ReasonPhrase = message;
            Request = request;
        }

        public HttpRequestMessage Request { get; set; }

        public string ReasonPhrase { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }
    }
}