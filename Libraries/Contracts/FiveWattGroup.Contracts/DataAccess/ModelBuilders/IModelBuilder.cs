using System.Data.Entity.ModelConfiguration.Configuration;

namespace FiveWattGroup.Contracts.DataAccess.ModelBuilders
{
    public interface IModelBuilder
    {
        void Configure(ConfigurationRegistrar registrar);
    }
}