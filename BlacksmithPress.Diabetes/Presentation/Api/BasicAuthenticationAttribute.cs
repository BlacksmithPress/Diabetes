using System;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using BlacksmithPress.Diabetes.Persistence.Database;

namespace BlacksmithPress.Diabetes.Cloud
{
    public class BasicAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var authorization = context.Request.Headers.Authorization;
            if (authorization == null)
                return;

            if (authorization.Scheme != "Basic")
                return;

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", context.Request);
                return;
            }

            var credentials = ExtractUserNameAndPassword(authorization.Parameter);
            if (credentials == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", context.Request);
                return;
            }

            using (var database = new Context("BlacksmithPress.Diabetes"))
            {
                var principal = database.Authenticate(credentials);
                if (principal == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", context.Request);
                    return;
                }

                context.Principal = principal;
            }
        }

        private NetworkCredential ExtractUserNameAndPassword(string parameter)
        {
            byte[] credentialBytes;

            try
            {
                credentialBytes = Convert.FromBase64String(parameter);
            }
            catch (FormatException)
            {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            var encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (string.IsNullOrEmpty(decodedCredentials))
            {
                return null;
            }

            var colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1)
            {
                return null;
            }

            var userName = decodedCredentials.Substring(0, colonIndex);
            var password = decodedCredentials.Substring(colonIndex + 1);
            return new NetworkCredential(userName, password);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue(AuthenticationTypes.Basic);
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }
    }
}