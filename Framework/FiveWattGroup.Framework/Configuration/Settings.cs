using System.Configuration;

using FiveWattGroup.Framework.Environments;

namespace FiveWattGroup.Framework.Configuration
{
    public static class Settings
    {
        public static bool AutomaticMigrationsEnabled
        {
            get
            {
                return true;
            }
        }

        public static bool AutomaticMigrationDataLossAllowed
        {
            get
            {
                return true;
            }
        }

        public static ConnectionStringSettings ConfigurationDbConnection
        {
            get
            {
                return ConnectionStringSettings["ConfigurationDbConnection"];
            }
        }

        public static string ConfigurationEndpoint
        {
            get
            {
                return "http://localhost:9090/Configuration";
            }
        }

        public static ConnectionStringSettings LoggingDbConnection
        {
            get
            {
                return ConnectionStringSettings["LoggingDbConnection"];
            }
        }

        public static string LoggingEndpoint
        {
            get
            {
                return "http://localhost:9090/Logging";
            }
        }

        public static string Environment
        {
            get
            {
                return EnvironmentName.Local;
            }
        }

        private static ConnectionStringSettingsCollection ConnectionStringSettings
        {
            get
            {
                var connStringSettings = new ConnectionStringSettingsCollection {
                    new ConnectionStringSettings { Name = "ConfigurationDbConnection", ConnectionString = "Data Source=.\\Backend; Initial Catalog=FiveWattGroupConfiguration; Integrated Security=true; MultipleActiveResultSets=true;", ProviderName = "System.Data.SqlClient" },
                    new ConnectionStringSettings { Name="LoggingDbConnection", ConnectionString = "Data Source=.\\Backend; Initial Catalog=FiveWattGroup; Integrated Security=true; MultipleActiveResultSets=true;", ProviderName = "System.Data.SqlClient" }
                };

                return connStringSettings;
            }
        }
    }
}
