using System;
using System.Linq.Expressions;
using System.Net.Http;

namespace FiveWattGroup.Contracts.Proxies.Http
{
    public interface IWebApiClient : IDisposable
    {
        HttpResponseMessage GetSync<TService>(Expression<Func<TService, HttpResponseMessage>> expression);
        HttpResponseMessage PostAsJsonSync<TService>(Expression<Func<TService, HttpResponseMessage>> expression);
    }
}