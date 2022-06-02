using System;
using System.Diagnostics.Contracts;

using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.BusinessObjects.Crud.Write
{
    public class CommandManager<TEntity> : ICommandOperations<TEntity> where TEntity : class
    {
        private ICommandOperations<TEntity> _commandRepository;
        private bool _disposed;

        public CommandManager(ICommandOperations<TEntity> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public TEntity Create(CommandRequest<TEntity> command)
        {
            Contract.Assume(command != null);
            Contract.Assume(command.Entity != null);
            Contract.Assume(_commandRepository != null);

            return _commandRepository.Create(command);
        }

        public TEntity Update(CommandRequest<TEntity> command)
        {
            Contract.Assume(command != null);
            Contract.Assume(command.Entity != null);
            Contract.Assume(_commandRepository != null);

            return _commandRepository.Update(command);
        }

        public TEntity Delete(CommandRequest<TEntity> command)
        {
            Contract.Assume(command != null);
            Contract.Assume(command.Entity != null);
            Contract.Assume(_commandRepository != null);

            return _commandRepository.Delete(command);
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
                    Contract.Assume(_commandRepository != null);

                    _commandRepository.Dispose();
                    _commandRepository = null;
                }
                finally
                {
                    _disposed = true;
                }
            }
        }
    }
}