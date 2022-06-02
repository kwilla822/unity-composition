using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace FiveWattGroup.Services.Common.ExceptionHandling
{
    internal class ErrorMessageResult : IHttpActionResult
    {
        private readonly HttpResponseMessage _httpResponseMessage;

        public ErrorMessageResult(HttpResponseMessage httpResponseMessage)
        {
            _httpResponseMessage = httpResponseMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_httpResponseMessage);
        }
    }
}