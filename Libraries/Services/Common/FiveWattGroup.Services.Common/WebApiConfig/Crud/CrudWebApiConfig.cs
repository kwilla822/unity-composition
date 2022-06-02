using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

using Newtonsoft.Json;

using FiveWattGroup.Services.Common.ExceptionHandling;
using FiveWattGroup.Services.Common.ModelBinders.Crud.Read;
using FiveWattGroup.Services.Common.ValueProviders.Crud.Read;

namespace FiveWattGroup.Services.Common.WebApiConfig.Crud
{
    public static class CrudWebApiConfig<TEntity, TDependencyResolver> where TEntity : class
        where TDependencyResolver : IDependencyResolver, new()
    {
        public static void Register(HttpConfiguration config)
        {
            // Base Web API configuration and services            
            config.DependencyResolver = new TDependencyResolver();

            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Add(typeof(ValueProviderFactory), new QueryRequestValueProviderFactory<TEntity>());
            config.Services.Add(typeof(ModelBinderProvider), new QueryRequestModelBinderProvider<TEntity>());
            
            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.All;

            // Base Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}",
                new {id = RouteParameter.Optional});
        }
    }
}