using System;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace FiveWattGroup.Entities.Pocos.Crud.Read
{
    public class QueryRequest<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Filter { get; set; }
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public Expression<Func<TEntity, object>> ThenBy { get; set; }
        public SortOrder SortDirection { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}