using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Contracts.Services.Logging;
using FiveWattGroup.Entities.CodeFirst.Logging;
using FiveWattGroup.Entities.Pocos.Crud.Write;
using FiveWattGroup.Services.Common.Controllers;

namespace FiveWattGroup.Services.Logging.Controllers
{
    public class LogController : BaseApiController, ILogService
    {
        private readonly ICommandOperations<Log> _commandManager;

        public LogController(ICommandOperations<Log> commandManager)
        {
            _commandManager = commandManager;
        }

        [HttpPost]
        public HttpResponseMessage Create(CommandRequest<Log> request)
        {
            var result = _commandManager.Create(request);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<Log>(result, new JsonMediaTypeFormatter())
            };
        }
    }
}
