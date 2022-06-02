using System.Web.Http;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Services.Common.WebApiConfig.Crud;

namespace FiveWattGroup.Services.WebApi.Configuration
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(CrudWebApiConfig<AppSetting, UnityManager>.Register);
        }
    }
}
