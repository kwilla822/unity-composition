using System.Data.Entity.ModelConfiguration.Configuration;

using FiveWattGroup.Contracts.DataAccess.ModelBuilders;
using FiveWattGroup.DataAccess.CodeFirst.TypeConfigurations.Configuration;

namespace FiveWattGroup.DataAccess.ModelBuilders.Configuration
{
    public class ConfigurationModelBuilder : IModelBuilder
    {
        public void Configure(ConfigurationRegistrar registrar)
        {
            //Register addtional TypeConfigurations for this model builder here.
            registrar.Add(new AppSettingTypeConfiguration());
        }
    }
}