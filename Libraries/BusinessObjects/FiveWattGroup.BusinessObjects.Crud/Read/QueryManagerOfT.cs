using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Read;

namespace FiveWattGroup.BusinessObjects.Crud.Read
{
    public class QueryManager<TEntity> : IQueryOperations<TEntity, IEnumerable<TEntity>> where TEntity : class
    {
        private bool _disposed;
        private IQueryOperations<TEntity, IQueryable<TEntity>> _queryRepository;

        public QueryManager(IQueryOperations<TEntity, IQueryable<TEntity>> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public IEnumerable<TEntity> Read(QueryRequest<TEntity> query)
        {
            Contract.Assume(query != null);
            Contract.Assume(_queryRepository != null);

            return _queryRepository.Read(query);
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
                    Contract.Assume(_queryRepository != null);

                    _queryRepository.Dispose();
                    _queryRepository = null;
                }
                finally
                {
                    _disposed = true;
                }
            }
        }
    }
}