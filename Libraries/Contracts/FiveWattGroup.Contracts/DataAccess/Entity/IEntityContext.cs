using System;
using System.Linq;

using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.Contracts.DataAccess.Entity
{
    public interface IEntityContext : IDisposable
    {
        IQueryable<TEntity> Read<TEntity>(QueryRequest<TEntity> query) where TEntity : class;
        TEntity Create<TEntity>(CommandRequest<TEntity> command) where TEntity : class;
        TEntity Update<TEntity>(CommandRequest<TEntity> command) where TEntity : class;
        TEntity Delete<TEntity>(CommandRequest<TEntity> command) where TEntity : class;
        int SaveChanges();
        void RejectChanges();
    }
}