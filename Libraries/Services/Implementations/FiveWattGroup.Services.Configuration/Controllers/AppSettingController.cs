using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Contracts.Services.Configuration;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;
using FiveWattGroup.Services.Common.Controllers;
using FiveWattGroup.Services.Common.ModelBinders.Crud.Read;

namespace FiveWattGroup.Services.Configuration.Controllers
{
    public class AppSettingController : BaseApiController, IAppSettingService
    {
        private readonly ICommandOperations<AppSetting> _commandManager;
        private readonly IQueryOperations<AppSetting, IEnumerable<AppSetting>> _queryManager;

        public AppSettingController(IQueryOperations<AppSetting, IEnumerable<AppSetting>> queryManager,
            ICommandOperations<AppSetting> commandManager)
        {
            _queryManager = queryManager;
            _commandManager = commandManager;
        }

        [HttpPost]
        public HttpResponseMessage ReadAsEnumerable([ModelBinder(typeof(QueryRequestModelBinderProvider<AppSetting>))]QueryRequest<AppSetting> request)
        {
            var results = _queryManager.Read(request);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<IEnumerable<AppSetting>>(results, new JsonMediaTypeFormatter())
            };
        }

        [HttpPost]
        public HttpResponseMessage ReadSingle([ModelBinder(typeof(QueryRequestModelBinderProvider<AppSetting>))]QueryRequest<AppSetting> request)
        {
            var result = _queryManager.Read(request).SingleOrDefault();

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<AppSetting>(result, new JsonMediaTypeFormatter())
            };
        }
        
        [HttpPost]
        public HttpResponseMessage Create(CommandRequest<AppSetting> request)
        {
            var result = _commandManager.Create(request);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<AppSetting>(result, new JsonMediaTypeFormatter())
            };
        }

        [HttpPost]
        public HttpResponseMessage Update(CommandRequest<AppSetting> request)
        {
            var result = _commandManager.Update(request);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<AppSetting>(result, new JsonMediaTypeFormatter())
            };
        }

        [HttpPost]
        public HttpResponseMessage Delete(CommandRequest<AppSetting> request)
        {
            var result = _commandManager.Delete(request);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<AppSetting>(result, new JsonMediaTypeFormatter())
            };
        }
    }
}