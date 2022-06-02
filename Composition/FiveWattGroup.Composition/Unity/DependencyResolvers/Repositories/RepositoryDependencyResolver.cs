using System.Linq;

using Microsoft.Practices.Unity;

using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Contracts.DataAccess.Entity;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.CodeFirst.Logging;
using FiveWattGroup.Repositories.Crud.Read;
using FiveWattGroup.Repositories.Crud.Write;

namespace FiveWattGroup.Composition.Unity.DependencyResolvers.Repositories
{
    internal static class RepositoryDependencyResolver
    {
        public static void RegisterRepositoryComponents(IUnityContainer container)
        {
            RegisterQueryRepositorySingleton<AppSetting>(container, "AppSettingQueryRepository", "Configuration");
            RegisterCommandRepositorySingleton<AppSetting>(container, "AppSettingCommandRepository", "Configuration");
            RegisterCommandRepositorySingleton<Log>(container, "LogCommandRepository", "Logging");
        }

        private static void RegisterQueryRepositorySingleton<TEntity>(IUnityContainer container, string registrationName,
            string entityContextRegistrationName) where TEntity : class
        {
            container.RegisterType<IQueryOperations<TEntity, IQueryable<TEntity>>, QueryRepository<TEntity>>(
                registrationName,
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IEntityContext>(entityContextRegistrationName)));
        }

        private static void RegisterCommandRepositorySingleton<TEntity>(IUnityContainer container,
            string registrationName, string entityContextRegistrationName) where TEntity : class
        {
            container.RegisterType<ICommandOperations<TEntity>, CommandRepository<TEntity>>(registrationName,
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IEntityContext>(entityContextRegistrationName)));
        }
    }
}
