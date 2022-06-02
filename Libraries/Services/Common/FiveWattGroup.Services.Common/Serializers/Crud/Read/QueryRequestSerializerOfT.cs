using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq.Expressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Services.Common.Serializers.Linq;

namespace FiveWattGroup.Services.Common.Serializers.Crud.Read
{
    public static class QueryRequestSerializer
    {
        public static QueryRequest<TEntity> Deserialize<TEntity>(string json) where TEntity : class
        {
            var requestObj = JsonConvert.DeserializeObject<JObject>(json);

            var filterValue = requestObj.Value<JObject>("Filter") == null ? null : ExpressionSerializer<TEntity>.Deserialize(requestObj.Value<JObject>("Filter"));
            var orderByValue = requestObj.Value<JObject>("OrderBy") == null ? null : ExpressionSerializer<TEntity>.Deserialize(requestObj.Value<JObject>("OrderBy"));
            var thenByValue = requestObj.Value<JObject>("ThenBy") == null ? null : ExpressionSerializer<TEntity>.Deserialize(requestObj.Value<JObject>("ThenBy"));
            var sortDirection = (SortOrder)Enum.Parse(typeof (SortOrder), requestObj.Value<int>("SortDirection").ToString(CultureInfo.InvariantCulture));

            var queryRequest = new QueryRequest<TEntity>
            {
                Filter = filterValue as Expression<Func<TEntity, bool>>,
                OrderBy = orderByValue as Expression<Func<TEntity, object>>,
                ThenBy = thenByValue as Expression<Func<TEntity, object>>,
                SortDirection = sortDirection,
                PageIndex = requestObj.Value<int?>("PageIndex"),
                PageSize = requestObj.Value<int?>("PageSize")
            };

            return queryRequest;
        }
    }
}
