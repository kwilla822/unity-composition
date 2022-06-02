using System.Data.Entity;

using FiveWattGroup.Contracts.DataAccess.Data;
using FiveWattGroup.Contracts.DataAccess.ModelBuilders;
using FiveWattGroup.DataAccess.Data;
using FiveWattGroup.DataAccess.ModelBuilders.Logging;
using FiveWattGroup.Framework.Configuration;

namespace FiveWattGroup.DataAccess.CodeFirst.Connections.Logging
{
    public class LoggingConnection : IDataContextConnection<DataContext<LoggingConnection>>
    {
        public string NameOrConnectionString
        {
            get
            {
                return Settings.LoggingDbConnection.ConnectionString;
            }
        }

        public IDatabaseInitializer<DataContext<LoggingConnection>> DatabaseInitializer
        {
            get { return null; }
        }

        public IModelBuilder ModelBuilder
        {
            get { return new LoggingModelBuilder(); }
        }
    }
}
