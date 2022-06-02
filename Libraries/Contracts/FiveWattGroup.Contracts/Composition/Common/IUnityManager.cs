using System;
using System.Collections.Generic;

namespace FiveWattGroup.Contracts.Composition.Common
{
    public interface IUnityManager : IDisposable
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
        T GetService<T>(string name = null);
    }
}
