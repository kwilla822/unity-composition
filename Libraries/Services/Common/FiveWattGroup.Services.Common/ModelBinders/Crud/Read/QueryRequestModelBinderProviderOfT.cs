using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FiveWattGroup.Entities.Pocos.Crud.Read;

namespace FiveWattGroup.Services.Common.ModelBinders.Crud.Read
{
    public class QueryRequestModelBinderProvider<TEntity> : ModelBinderProvider where TEntity : class
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if (modelType == typeof(QueryRequest<TEntity>))
            {
                return new QueryRequestModelBinder();
            }

            return null;
        }
    }
}
