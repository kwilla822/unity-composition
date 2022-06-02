using System.Net.Http;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Contracts.Proxies.Http;
using FiveWattGroup.Contracts.Services.Logging;
using FiveWattGroup.Entities.CodeFirst.Logging;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.Diagnostics.Logging
{
    public static class LogManager
    {
        private static readonly IWebApiClient Proxy;

        static LogManager()
        {
            Proxy = new UnityManager().GetService<IWebApiClient>("LogClient");
        }

        public static Log WriteLogEntry(CommandRequest<Log> command)
        {
            var response = Proxy.PostAsJsonSync<ILogService>(service => service.Create(command));
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Log>().Result;
        }
    }
}
