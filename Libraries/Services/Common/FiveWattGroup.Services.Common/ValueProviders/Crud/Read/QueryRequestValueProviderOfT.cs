using System.Globalization;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

using FiveWattGroup.Services.Common.Serializers.Crud.Read;

namespace FiveWattGroup.Services.Common.ValueProviders.Crud.Read
{
    public class QueryRequestValueProvider<TEntity> : IValueProvider where TEntity : class
    {
        private readonly HttpActionContext _context;

        public QueryRequestValueProvider(HttpActionContext context)
        {
            _context = context;
        }

        public bool ContainsPrefix(string prefix)
        {
            return prefix == "request";
        }

        public ValueProviderResult GetValue(string key)
        {
            var json = _context.Request.Content.ReadAsStringAsync().Result;
            var queryRequest = QueryRequestSerializer.Deserialize<TEntity>(json);

            return new ValueProviderResult(queryRequest, json, CultureInfo.InvariantCulture);
        }
    }
}
