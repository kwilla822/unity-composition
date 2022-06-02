using System.Net.Http;
using System.Web.Http;

using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.Contracts.Services.Configuration
{
    public interface IAppSettingService
    {
        [HttpPost]
        [Route("api/AppSetting/ReadAsEnumerable")]
        HttpResponseMessage ReadAsEnumerable(QueryRequest<AppSetting> request);

        [HttpPost]
        [Route("api/AppSetting/ReadSingle")]
        HttpResponseMessage ReadSingle(QueryRequest<AppSetting> request);

        [HttpPost]
        [Route("api/AppSetting/Create")]
        HttpResponseMessage Create(CommandRequest<AppSetting> request);

        [HttpPost]
        [Route("api/AppSetting/Update")]
        HttpResponseMessage Update(CommandRequest<AppSetting> request);

        [HttpPost]
        [Route("api/AppSetting/Delete")]
        HttpResponseMessage Delete(CommandRequest<AppSetting> request);
    }
}