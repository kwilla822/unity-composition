using System.Data.Entity;

using FiveWattGroup.Contracts.DataAccess.ModelBuilders;

namespace FiveWattGroup.Contracts.DataAccess.Data
{
    public interface IDataContextConnection<in TContext> where TContext : DbContext
    {
        string NameOrConnectionString { get; }
        IDatabaseInitializer<TContext> DatabaseInitializer { get; }
        IModelBuilder ModelBuilder { get; }
    }
}
