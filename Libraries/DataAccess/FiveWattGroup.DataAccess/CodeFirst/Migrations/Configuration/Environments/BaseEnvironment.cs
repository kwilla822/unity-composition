using System.Collections.Generic;

using FiveWattGroup.Contracts.DataAccess.Data;
using FiveWattGroup.DataAccess.Data;

namespace FiveWattGroup.DataAccess.CodeFirst.Migrations.Configuration.Environments
{
    public abstract class BaseEnvironment
    {
        public virtual void Seed<TEntity, TContextConnection>(DataContext<TContextConnection> context, List<TEntity> seedItems) 
            where TEntity : class
            where TContextConnection : IDataContextConnection<DataContext<TContextConnection>>, new()
        {
            seedItems.ForEach(item =>
            {
                var existingItem = context.Find(item);
                if (existingItem == null)
                    context.Set<TEntity>().Add(item);
            });

            context.SaveChanges();
        }
    }
}
