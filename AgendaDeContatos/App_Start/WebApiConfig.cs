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
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
/*

            config.Routes.MapHttpRoute(
                name: "ContatosTelefoneApi",
                routeTemplate: "api/contatos/{contatoId}/telefones/{id}",
                defaults: new { controller = "telefones", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ContatosApi",
                routeTemplate: "api/contatos/{id}",
                defaults: new { controller = "contatos", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "TokenApi",
                routeTemplate: "api/token/{id}",
                defaults: new { controller = "token", id = RouteParameter.Optional }
            );
*/

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            /*var formatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            config.Formatters.Insert(0, formatter);*/

            CreateMediaTypes(config.Formatters.JsonFormatter);

            //config.Services.Replace(typeof(IHttpControllerSelector), new MyControllerSelector(config));

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
