using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

using Microsoft.Practices.Unity;

using FiveWattGroup.Composition.Unity.DependencyResolvers.BusinessObjects;
using FiveWattGroup.Composition.Unity.DependencyResolvers.DataAccess;
using FiveWattGroup.Composition.Unity.DependencyResolvers.Proxies;
using FiveWattGroup.Composition.Unity.DependencyResolvers.Repositories;
using FiveWattGroup.Composition.Unity.DependencyResolvers.Services;
using FiveWattGroup.Contracts.Composition.Common;

namespace FiveWattGroup.Composition.Unity.Common
{
    public class UnityManager : IDependencyResolver, IUnityManager
    {
        private IUnityContainer _container;
        private bool _disposed;

        public UnityManager()
        {
            var container = new UnityContainer();

            DataAccessDependencyResolver.RegisterDataAccessComponents(container);
            RepositoryDependencyResolver.RegisterRepositoryComponents(container);
            BusinessObjectDependencyResolver.RegisterBusinessObjectComponents(container);
            ProxyDependencyResolver.RegisterProxyComponents(container);
            ServiceDependencyResolver.RegisterServiceComponents(container);

            _container = container;
        }

        public UnityManager(IUnityContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            IUnityContainer child = _container.CreateChildContainer();
            return new UnityManager(child);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return Enumerable.Empty<object>();
            }
        }

        public T GetService<T>(string name = null)
        {
            try
            {
                return _container.Resolve<T>(name);
            }
            catch (ResolutionFailedException)
            {
                return default(T);
            }
        }

        public IUnityContainer GetContainer()
        {
            return _container;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    _container.Dispose();
                    _container = null;
                }
                finally
                {
                    _disposed = true;
                }
            }
        }
    }
}
