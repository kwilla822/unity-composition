using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FiveWattGroup.Services.Common.Controllers
{
    public class BaseApiController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content =
                    new StringContent(string.Format("'{0}' Is Running.  The Current Date/Time Is: {1}",
                        GetType()
                            .GetInterfaces()
                            .First(i => i.Assembly.GetName().Name.Contains("Contracts"))
                            .Name.Replace("I", string.Empty), DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss")))
            };
        }
    }
}
