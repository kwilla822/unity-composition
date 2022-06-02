using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

using FiveWattGroup.Contracts.DataAccess.Data;
using FiveWattGroup.Contracts.DataAccess.Entity;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.DataAccess.Entity
{
    public class EntityContext : IEntityContext
    {
        private bool _disposed;
        private IDbContext _context;

        public EntityContext(IDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Read<TEntity>(QueryRequest<TEntity> query) where TEntity : class
        {
            var queryable = _context.SetOf<TEntity>().AsQueryable();

            queryable = TryApplyPredicate(queryable, query);
            queryable = TryApplyOrdering(queryable, query);
            queryable = TryApplyPaging(queryable, query);

            return queryable;
        }

        public TEntity Create<TEntity>(CommandRequest<TEntity> command) where TEntity : class
        {
            var current = _context.Find(command.Entity);
            var created = current ?? _context.SetOf<TEntity>().Add(command.Entity);

            if (command.SaveChanges)
                SaveChanges();

            return created;
        }

        public TEntity Update<TEntity>(CommandRequest<TEntity> command) where TEntity : class
        {
            var current = _context.Find(command.Entity);

            current = _context.CopyValues(current, command.Entity);
            _context.SetState(current, EntityState.Modified);

            if (command.SaveChanges)
                SaveChanges();

            return current;
        }

        public TEntity Delete<TEntity>(CommandRequest<TEntity> command) where TEntity : class
        {
            var current = _context.Find(command.Entity);

            _context.SetOf<TEntity>().Remove(current);

            if (command.SaveChanges)
                SaveChanges();

            return current;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void RejectChanges()
        {
            _context.RejectChanges();
        }

        private IQueryable<TEntity> TryApplyPredicate<TEntity>(IQueryable<TEntity> queryable, QueryRequest<TEntity> query) where TEntity : class
        {
            if (query.Filter != null)
            {
                queryable = queryable.Where(query.Filter);
            }

            return queryable;
        }

        private IQueryable<TEntity> TryApplyOrdering<TEntity>(IQueryable<TEntity> queryable, QueryRequest<TEntity> query) where TEntity : class
        {
            if (query.OrderBy != null)
            {
                queryable = query.SortDirection == SortOrder.Descending
                    ? queryable.OrderByDescending(query.OrderBy)
                    : queryable.OrderBy(query.OrderBy);

                if (query.ThenBy != null)
                {
                    queryable = query.SortDirection == SortOrder.Descending
                        ? ((IOrderedQueryable<TEntity>)queryable).ThenByDescending(query.ThenBy)
                        : ((IOrderedQueryable<TEntity>)queryable).ThenBy(query.ThenBy);
                }
            }

            return queryable;
        }

        private IQueryable<TEntity> TryApplyPaging<TEntity>(IQueryable<TEntity> queryable, QueryRequest<TEntity> query) where TEntity : class
        {
            if (query.PageIndex.HasValue && query.PageSize.HasValue)
            {
                int pageIndex = Math.Max(1, query.PageIndex.Value);
                int pageSize = Math.Max(25, query.PageSize.Value);

                queryable = queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            return queryable;
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