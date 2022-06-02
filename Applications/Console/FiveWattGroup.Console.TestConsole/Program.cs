using System.Linq;
using System.Net.Http;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Contracts.Proxies.Http;
using FiveWattGroup.Contracts.Services.Configuration;
using FiveWattGroup.Diagnostics.Configuration;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Contracts.DataAccess.Entity;
using FiveWattGroup.Contracts.Crud.Read;
using System.Collections.Generic;

namespace FiveWattGroup.Console.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = AppSettingManager.GetAppSetting("TestKey");
        }
    }
}
