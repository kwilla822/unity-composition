using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace FiveWattGroup.Services.Common.ModelBinders.Crud.Read
{
    public class QueryRequestModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.Model = value.RawValue;

            return true;
        }
    }
}