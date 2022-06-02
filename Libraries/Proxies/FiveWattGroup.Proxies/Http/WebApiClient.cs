using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;

using Newtonsoft.Json;

using FiveWattGroup.Contracts.Proxies.Http;
using FiveWattGroup.Core.Extensions;
using FiveWattGroup.Entities.Enums.Proxies.Http;
using FiveWattGroup.Entities.Pocos.Proxies.Http;

namespace FiveWattGroup.Proxies.Http
{
    public class WebApiClient : IWebApiClient
    {
        private HttpClient _client;
        private bool _disposed;

        public WebApiClient(WebApiClientContext context)
        {
            _client = context.Handler == null
                ? new HttpClient()
                : new HttpClient(context.Handler);
            _client.BaseAddress = new Uri(context.BaseAddress).Append("/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpResponseMessage GetSync<TService>(Expression<Func<TService, HttpResponseMessage>> methodExpression)
        {
            var methodCallExpression = TryGetMethodCallExpression(methodExpression);
            var routeAttribute = TryGetRouteAttribute(methodCallExpression);
            var httpAttribute = TryGetHttpActionAttribute(methodCallExpression);

            if (httpAttribute != HttpAttributeTypeCode.Get)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = "The Service Operation Must Be Decorated With [HttpGet] Attribute."
                };
            }

            var args = ExtractArgumentsFromMethodCallExpression(methodCallExpression);
            var argsUriString = args == null ? string.Empty : string.Join("/", args);
            var requestUri = string.IsNullOrWhiteSpace(argsUriString)
                ? routeAttribute.Template
                : new Uri(routeAttribute.Template).Append(argsUriString).AbsoluteUri;

            var response = _client.GetAsync(requestUri).Result;

            return response;
        }

        public HttpResponseMessage PostAsJsonSync<TService>(Expression<Func<TService, HttpResponseMessage>> methodExpression)
        {
            var methodCallExpression = TryGetMethodCallExpression(methodExpression);
            var routeAttribute = TryGetRouteAttribute(methodCallExpression);
            var httpAttribute = TryGetHttpActionAttribute(methodCallExpression);

            if (httpAttribute != HttpAttributeTypeCode.Post)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = "The Service Operation Must Be Decorated With [HttpPost] Attribute."
                };
            }

            var args = ExtractArgumentsFromMethodCallExpression(methodCallExpression).ToList();
            if (args.Count > 1)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase =
                        string.Format("'{0}' Only Supports A Single Request Argument.",
                            MethodBase.GetCurrentMethod().Name)
                };
            }

            object postArg = args.SingleOrDefault();

            HttpResponseMessage response =
                _client.PostAsync(routeAttribute.Template,
                    new ObjectContent(postArg.GetType(), postArg,
                        new JsonMediaTypeFormatter
                        {
                            SerializerSettings =
                                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All}
                        })).Result;

            return response;
        }

        private MethodCallExpression TryGetMethodCallExpression<TService>(
            Expression<Func<TService, HttpResponseMessage>> methodExpression)
        {
            var methodCallExpression = methodExpression.Body as MethodCallExpression;
            return methodCallExpression;
        }

        private RouteAttribute TryGetRouteAttribute(MethodCallExpression expression)
        {
            var routeAttr = expression.Method.GetCustomAttribute<RouteAttribute>();
            return routeAttr;
        }

        private HttpAttributeTypeCode TryGetHttpActionAttribute(MethodCallExpression expression)
        {
            Attribute httpAttribute =
                expression.Method.GetCustomAttributes()
                    .SingleOrDefault(
                        x =>
                            x is HttpGetAttribute ||
                            x is HttpPostAttribute ||
                            x is HttpPutAttribute ||
                            x is HttpDeleteAttribute);

            if (httpAttribute is HttpPostAttribute)
                return HttpAttributeTypeCode.Post;

            if (httpAttribute is HttpPutAttribute)
                return HttpAttributeTypeCode.Put;

            if (httpAttribute is HttpDeleteAttribute)
                return HttpAttributeTypeCode.Delete;

            return HttpAttributeTypeCode.Get;
        }

        private IEnumerable<object> ExtractArgumentsFromMethodCallExpression(MethodCallExpression expression)
        {
            IEnumerable<object> args = (from arg in expression.Arguments
                let argAsObj = Expression.Convert(arg, typeof (object))
                select Expression.Lambda<Func<object>>(argAsObj, null)
                    .Compile()()).AsEnumerable();

            return args;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    _client.Dispose();
                    _client = null;
                }
                finally
                {
                    _disposed = true;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}