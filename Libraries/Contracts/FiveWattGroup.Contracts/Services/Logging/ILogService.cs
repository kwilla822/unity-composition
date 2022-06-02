using System.Net.Http;
using System.Web.Http;

using FiveWattGroup.Entities.CodeFirst.Logging;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.Contracts.Services.Logging
{
    public interface ILogService
    {
        [HttpPost]
        [Route("api/Log/Create")]
        HttpResponseMessage Create(CommandRequest<Log> request);
    }
}
