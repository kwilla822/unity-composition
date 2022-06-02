using System.Data.Entity.ModelConfiguration.Configuration;

using FiveWattGroup.Contracts.DataAccess.ModelBuilders;
using FiveWattGroup.DataAccess.CodeFirst.TypeConfigurations.Logging;

namespace FiveWattGroup.DataAccess.ModelBuilders.Logging
{
    public class LoggingModelBuilder : IModelBuilder
    {
        public void Configure(ConfigurationRegistrar registrar)
        {
            //Register addtional TypeConfigurations for this model builder here.
            registrar.Add(new LogTypeConfiguration());
        }
    }
}