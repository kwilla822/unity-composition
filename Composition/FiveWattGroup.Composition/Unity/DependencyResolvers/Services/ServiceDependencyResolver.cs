using System.Collections.Generic;

using Microsoft.Practices.Unity;

using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Contracts.Services.Configuration;
using FiveWattGroup.Contracts.Services.Logging;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.CodeFirst.Logging;
using FiveWattGroup.Services.Configuration.Controllers;
using FiveWattGroup.Services.Logging.Controllers;

namespace FiveWattGroup.Composition.Unity.DependencyResolvers.Services
{
    internal static class ServiceDependencyResolver
    {
        public static void RegisterServiceComponents(IUnityContainer container)
        {
            RegisterCrudServicePerRequest<AppSetting, IAppSettingService, AppSettingController>(container,
                "AppSettingQueryManager", "AppSettingCommandManager");
            RegisterCommandServicePerRequest<Log, ILogService, LogController>(container, "LogCommandManager");
        }

        private static void RegisterQueryServicePerRequest<TEntity, TServiceContract, TServiceController>(
            IUnityContainer container, string queryManagerRegistrationName)
            where TEntity : class
            where TServiceContract : class
            where TServiceController : TServiceContract
        {
            container.RegisterType<TServiceContract, TServiceController>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IQueryOperations<TEntity, IEnumerable<TEntity>>>(queryManagerRegistrationName)));
        }

        private static void RegisterCommandServicePerRequest<TEntity, TServiceContract, TServiceController>(
            IUnityContainer container, string commandManagerRegistrationName)
            where TEntity : class
            where TServiceContract : class
            where TServiceController : TServiceContract
        {
            container.RegisterType<TServiceContract, TServiceController>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ICommandOperations<TEntity>>(commandManagerRegistrationName)));
        }

        private static void RegisterCrudServicePerRequest<TEntity, TServiceContract, TServiceController>(IUnityContainer container, string queryManagerRegistrationName, string commandManagerRegistrationName)
            where TEntity : class
            where TServiceContract : class
            where TServiceController : TServiceContract
        {
            container.RegisterType<TServiceContract, TServiceController>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IQueryOperations<TEntity, IEnumerable<TEntity>>>(
                        queryManagerRegistrationName),
                    new ResolvedParameter<ICommandOperations<TEntity>>(commandManagerRegistrationName)));
        }
    }
}
