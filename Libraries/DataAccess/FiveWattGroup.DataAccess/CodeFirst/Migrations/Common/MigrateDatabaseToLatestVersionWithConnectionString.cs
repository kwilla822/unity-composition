using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

using FiveWattGroup.Contracts.DataAccess.Data;
using FiveWattGroup.DataAccess.Data;

namespace FiveWattGroup.DataAccess.CodeFirst.Migrations.Common
{
    internal class MigrateDatabaseToLatestVersionWithConnectionString<TContextConnection, TMigrationsConfiguration> : IDatabaseInitializer<DataContext<TContextConnection>>
        where TContextConnection : IDataContextConnection<DataContext<TContextConnection>>, new()
        where TMigrationsConfiguration : DbMigrationsConfiguration<DataContext<TContextConnection>>, new()
    {
        private readonly DbMigrationsConfiguration _config;

        public MigrateDatabaseToLatestVersionWithConnectionString()
        {
            _config = new TMigrationsConfiguration();
        }

        public MigrateDatabaseToLatestVersionWithConnectionString(string connectionString)
        {
            _config = new TMigrationsConfiguration
            {
                TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient")
            };
        }

        public void InitializeDatabase(DataContext<TContextConnection> context)
        {
            var dbMigrator = new DbMigrator(_config);
            dbMigrator.Update();
        }
    }
}