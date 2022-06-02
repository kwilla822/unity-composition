using System.Data.Entity;

using FiveWattGroup.Contracts.DataAccess.Data;
using FiveWattGroup.Contracts.DataAccess.ModelBuilders;
using FiveWattGroup.DataAccess.CodeFirst.Migrations.Common;
using FiveWattGroup.DataAccess.CodeFirst.Migrations.Configuration;
using FiveWattGroup.DataAccess.Data;
using FiveWattGroup.DataAccess.ModelBuilders.Configuration;
using FiveWattGroup.Framework.Configuration;

namespace FiveWattGroup.DataAccess.CodeFirst.Connections.Configuration
{
    public class ConfigurationConnection : IDataContextConnection<DataContext<ConfigurationConnection>>
    {
        public string NameOrConnectionString
        {
            get
            {
                return Settings.ConfigurationDbConnection.ConnectionString;
            }
        }
        
        public IDatabaseInitializer<DataContext<ConfigurationConnection>> DatabaseInitializer
        {
            get { return new MigrateDatabaseToLatestVersionWithConnectionString<ConfigurationConnection, ConfigurationMigration>(); }
        }

        public IModelBuilder ModelBuilder
        {
            get { return new ConfigurationModelBuilder(); }
        }
    }
}
