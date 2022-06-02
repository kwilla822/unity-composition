using System;
using System.Data.Entity;

namespace FiveWattGroup.Contracts.DataAccess.Data
{
    public interface IDbContext : IDisposable 
    {
        IDbSet<TEntity> SetOf<TEntity>() where TEntity : class; 
        int SaveChanges();
        void RejectChanges();
        void SetState<TEntity>(TEntity entity, EntityState state) where TEntity : class;
        TEntity Find<TEntity>(TEntity entity) where TEntity : class;
        TEntity CopyValues<TEntity>(TEntity current, TEntity delta) where TEntity : class;
    }
}
