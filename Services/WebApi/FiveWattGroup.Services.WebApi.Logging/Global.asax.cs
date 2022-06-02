using System.Web.Http;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Entities.CodeFirst.Logging;
using FiveWattGroup.Services.Common.WebApiConfig.Crud;

namespace FiveWattGroup.Services.WebApi.Logging
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(CrudWebApiConfig<Log, UnityManager>.Register);
        }
    }
}
