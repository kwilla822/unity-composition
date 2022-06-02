using System;
using System.Collections.Generic;

using FiveWattGroup.Entities.Pocos.Crud.Read;

namespace FiveWattGroup.Contracts.Crud.Read
{
    public interface IQueryOperations<TEntity, out TReturn> : IDisposable
        where TEntity : class
        where TReturn : IEnumerable<TEntity>
    {
        TReturn Read(QueryRequest<TEntity> query);
    }
}