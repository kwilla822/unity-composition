using System;

using Microsoft.Practices.Unity;

using FiveWattGroup.Contracts.Proxies.Http;
using FiveWattGroup.Entities.Pocos.Proxies.Http;
using FiveWattGroup.Proxies.Http;
using FiveWattGroup.Framework.Configuration;

namespace FiveWattGroup.Composition.Unity.DependencyResolvers.Proxies
{
    internal static class ProxyDependencyResolver
    {
        public static void RegisterProxyComponents(IUnityContainer container)
        {
            container.RegisterType<IWebApiClient, WebApiClient>("AppSettingClient", new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new InjectionParameter(new WebApiClientContext
                {
                    BaseAddress = Settings.ConfigurationEndpoint
                })));

            container.RegisterType<IWebApiClient, WebApiClient>("LogClient", new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new InjectionParameter(new WebApiClientContext
                {
                    BaseAddress = Settings.LoggingEndpoint
                })));
        }
    }
}
