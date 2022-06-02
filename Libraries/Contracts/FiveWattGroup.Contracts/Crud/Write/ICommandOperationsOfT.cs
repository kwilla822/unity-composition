using System;

using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.Contracts.Crud.Write
{
    public interface ICommandOperations<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(CommandRequest<TEntity> command);
        TEntity Update(CommandRequest<TEntity> command);
        TEntity Delete(CommandRequest<TEntity> command);
    }
}