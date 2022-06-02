using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

using FiveWattGroup.Contracts.DataAccess.Data;

namespace FiveWattGroup.DataAccess.Data
{
    public class DataContext<TContextConnection> : DbContext, IDbContext
        where TContextConnection : IDataContextConnection<DataContext<TContextConnection>>, new()
    {
        private static readonly TContextConnection ConnectionInfo = new TContextConnection();

        public DataContext() : base(ConnectionInfo.NameOrConnectionString)
        {
            if (ConnectionInfo.DatabaseInitializer != null)
            {
                Database.SetInitializer(ConnectionInfo.DatabaseInitializer);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConnectionInfo.ModelBuilder.Configure(modelBuilder.Configurations);
        }

        public IDbSet<TEntity> SetOf<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public void SetState<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            Entry(entity).State = state;
        }

        public TEntity CopyValues<TEntity>(TEntity current, TEntity delta) where TEntity : class
        {
            Entry(current).CurrentValues.SetValues(delta);
            return current;
        }

        public TEntity Find<TEntity>(TEntity entity) where TEntity : class
        {
            object[] keyValues = GetKeyValues(entity);
            TEntity existingEntity = Set<TEntity>().Find(keyValues);

            return existingEntity;
        }

        public void RejectChanges()
        {
            ChangeTracker.Entries().ToList().ForEach(entry =>
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            });
        }

        private List<string> GetKeyNames<TEntity>() where TEntity : class
        {
            ObjectSet<TEntity> objectSet = ((IObjectContextAdapter) this).ObjectContext.CreateObjectSet<TEntity>();
            List<string> keyNames = objectSet.EntitySet.ElementType.KeyMembers.Select(k => k.Name).ToList();

            return keyNames;
        }

        private object[] GetKeyValues<TEntity>(TEntity entity) where TEntity : class
        {
            var keyValues = new List<object>();

            Type entityType = typeof (TEntity);
            List<string> keyNames = GetKeyNames<TEntity>();
            keyNames.ForEach(keyName =>
            {
                object value = entityType.GetProperty(keyName).GetValue(entity, null);
                keyValues.Add(value);
            });

            return keyValues.ToArray();
        }
    }
}