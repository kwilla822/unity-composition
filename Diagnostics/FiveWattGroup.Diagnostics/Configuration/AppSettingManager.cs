using System.Collections.Generic;
using System.Net.Http;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Contracts.Proxies.Http;
using FiveWattGroup.Contracts.Services.Configuration;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.Diagnostics.Configuration
{
    public static class AppSettingManager
    {
        private static readonly IWebApiClient Proxy;

        static AppSettingManager()
        {
            Proxy = new UnityManager().GetService<IWebApiClient>("AppSettingClient");
        }

        public static IEnumerable<AppSetting> GetAppSettings(QueryRequest<AppSetting> query)
        {
            var response = Proxy.PostAsJsonSync<IAppSettingService>(service => service.ReadAsEnumerable(query));
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<List<AppSetting>>().Result;
        }

        public static AppSetting GetAppSetting(string configKey)
        { 
            var response =
                Proxy.PostAsJsonSync<IAppSettingService>(
                    service => service.ReadSingle(new QueryRequest<AppSetting> {Filter = x => x.ConfigKey == configKey}));
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<AppSetting>().Result;
        }

        public static AppSetting Create(CommandRequest<AppSetting> command)
        {
            var response = Proxy.PostAsJsonSync<IAppSettingService>(service => service.Create(command));
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<AppSetting>().Result;
        }

        public static AppSetting Update(CommandRequest<AppSetting> command)
        {
            var response = Proxy.PostAsJsonSync<IAppSettingService>(service => service.Update(command));
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<AppSetting>().Result;
        }

        public static AppSetting Delete(CommandRequest<AppSetting> command)
        {
            var response = Proxy.PostAsJsonSync<IAppSettingService>(service => service.Delete(command));
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<AppSetting>().Result;
        }
    }
}
