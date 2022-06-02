using System;
using System.Collections.Generic;

using FiveWattGroup.DataAccess.CodeFirst.Connections.Configuration;
using FiveWattGroup.DataAccess.Data;
using FiveWattGroup.Entities.CodeFirst.Configuration;

namespace FiveWattGroup.DataAccess.CodeFirst.Migrations.Configuration.Environments
{
    public class Production : BaseEnvironment
    {
        public void Seed(DataContext<ConfigurationConnection> context)
        {
            var settings = new List<AppSetting>
            {
                new AppSetting
                {
                    ConfigKey = "TestKey",
                    ConfigValue = "TestValue",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "kwilla",
                    LastModifiedDate = DateTimeOffset.Now
                }
            };

            base.Seed(context, settings);
        }
    }
}
