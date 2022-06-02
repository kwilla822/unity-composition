using System.Net.Http;

namespace FiveWattGroup.Entities.Pocos.Proxies.Http
{
    public class WebApiClientContext
    {
        public string BaseAddress { get; set; }
        public HttpClientHandler Handler { get; set; }
    }
}