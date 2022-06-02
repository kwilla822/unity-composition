using System;

using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Contracts.DataAccess.Entity;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.Repositories.Crud.Write
{
    public class CommandRepository<TEntity> : ICommandOperations<TEntity> where TEntity : class
    {
        private IEntityContext _context;
        private bool _disposed;

        public CommandRepository(IEntityContext context)
        {
            _context = context;
        }

        public TEntity Create(CommandRequest<TEntity> command)
        {
            return _context.Create(command);
        }

        public TEntity Update(CommandRequest<TEntity> command)
        {
            return _context.Update(command);;
        }

        public TEntity Delete(CommandRequest<TEntity> command)
        {
            return _context.Delete(command);
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