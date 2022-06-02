using Microsoft.Practices.Unity;

using FiveWattGroup.Contracts.DataAccess.Data;
using FiveWattGroup.Contracts.DataAccess.Entity;
using FiveWattGroup.DataAccess.CodeFirst.Connections.Configuration;
using FiveWattGroup.DataAccess.CodeFirst.Connections.Logging;
using FiveWattGroup.DataAccess.Data;
using FiveWattGroup.DataAccess.Entity;

namespace FiveWattGroup.Composition.Unity.DependencyResolvers.DataAccess
{
    internal static class DataAccessDependencyResolver
    {
        public static void RegisterDataAccessComponents(IUnityContainer container)
        {
            RegisterEntityContext<ConfigurationConnection>(container, "Configuration");
            RegisterEntityContext<LoggingConnection>(container, "Logging");
        }

        private static void RegisterEntityContext<TContextConnection>(IUnityContainer container, string registrationName) where TContextConnection : IDataContextConnection<DataContext<TContextConnection>>, new()
        {
            container.RegisterType<IDbContext, DataContext<TContextConnection>>(registrationName, new ContainerControlledLifetimeManager());
            container.RegisterType<IEntityContext, EntityContext>(registrationName,
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IDbContext>(registrationName)));
        }
    }
}
