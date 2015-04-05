using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using AgendaDeContatos.Servicos;
using Newtonsoft.Json;
using WebApiContrib.Formatting.Jsonp;

namespace AgendaDeContatos
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;


            CreateMediaTypes(config.Formatters.JsonFormatter);

            config.Services.Replace(typeof(IHttpControllerSelector), new ApiControllerSelector(config));

            config.EnableCors();
        }

        static void CreateMediaTypes(JsonMediaTypeFormatter jsonFormatter)
        {
            new[]
            {
                "application/vnd.agendadecontatos.contatos.v1+json",
                "application/vnd.agendadecontatos.contatos.v2+json",
                "application/vnd.agendadecontatos.telefones.v1+json",
            }
                .Select(m => new MediaTypeHeaderValue(m))
                .ToList()
                .ForEach(jsonFormatter.SupportedMediaTypes.Add);
        }
    }
}
