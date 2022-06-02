using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;

namespace FiveWattGroup.Services.Common.ExceptionHandling
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(context.Exception.ToString()),
                ReasonPhrase = "Unhandled Exception"
            };

            context.Result = new ErrorMessageResult(response);
        }
    }
}
