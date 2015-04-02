using System.Web.Http;
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

            config.Routes.MapHttpRoute(
                name: "ContatosTelefoneApi",
                routeTemplate: "api/contatos/{contatoId}/telefones/{id}",
                defaults: new { controller = "telefones", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ContatosApiV1",
                routeTemplate: "api/v1/contatos/{id}",
                defaults: new { controller = "contatos", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ContatosApiV2",
                routeTemplate: "api/v2/contatos/{id}",
                defaults: new { controller = "contatosv2", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "TokenApi",
                routeTemplate: "api/token/{id}",
                defaults: new { controller = "token", id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            /*var formatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            config.Formatters.Insert(0, formatter);*/

            config.EnableCors();
        }
    }
}
