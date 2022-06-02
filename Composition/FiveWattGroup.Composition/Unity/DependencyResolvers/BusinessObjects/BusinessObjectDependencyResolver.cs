using System.Collections.Generic;
using System.Linq;

using Microsoft.Practices.Unity;

using FiveWattGroup.BusinessObjects.Crud.Read;
using FiveWattGroup.BusinessObjects.Crud.Write;
using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.CodeFirst.Logging;

namespace FiveWattGroup.Composition.Unity.DependencyResolvers.BusinessObjects
{
    internal static class BusinessObjectDependencyResolver
    {
        public static void RegisterBusinessObjectComponents(IUnityContainer container)
        {
            RegisterQueryManagerSingleton<AppSetting>(container, "AppSettingQueryManager", "AppSettingQueryRepository");
            RegisterCommandManagerSingleton<AppSetting>(container, "AppSettingCommandManager", "AppSettingCommandRepository");
            RegisterCommandManagerSingleton<Log>(container, "LogCommandManager", "LogCommandRepository");
        }

        private static void RegisterQueryManagerSingleton<TEntity>(IUnityContainer container, string registrationName,
            string repositoryRegistrationName) where TEntity : class
        {
            container.RegisterType<IQueryOperations<TEntity, IEnumerable<TEntity>>, QueryManager<TEntity>>(
                registrationName,
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IQueryOperations<TEntity, IQueryable<TEntity>>>(repositoryRegistrationName)));
        }

        private static void RegisterCommandManagerSingleton<TEntity>(IUnityContainer container,
            string registrationName, string repositoryRegistrationName) where TEntity : class
        {
            container.RegisterType<ICommandOperations<TEntity>, CommandManager<TEntity>>(registrationName,
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<ICommandOperations<TEntity>>(repositoryRegistrationName)));
        }
    }
}
