using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace FiveWattGroup.Services.Common.ValueProviders.Crud.Read
{
    public class QueryRequestValueProviderFactory<TEntity> : ValueProviderFactory where TEntity : class
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new QueryRequestValueProvider<TEntity>(actionContext);
        }
    }
}