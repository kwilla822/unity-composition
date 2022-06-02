using System;
using System.Linq;

using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Contracts.DataAccess.Entity;
using FiveWattGroup.Entities.Pocos.Crud.Read;

namespace FiveWattGroup.Repositories.Crud.Read
{
    public class QueryRepository<TEntity> : IQueryOperations<TEntity, IQueryable<TEntity>> where TEntity : class
    {
        private IEntityContext _context;
        private bool _disposed;

        public QueryRepository(IEntityContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Read(QueryRequest<TEntity> query)
        {
            return _context.Read(query);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    _context.Dispose();
                    _context = null;
                }
                finally
                {
                    _disposed = true;
                }
            }
        }
    }
}