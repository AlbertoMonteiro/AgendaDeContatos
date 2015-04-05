using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace AgendaDeContatos.Servicos
{
    public class ApiControllerSelector : DefaultHttpControllerSelector
    {
        readonly HttpConfiguration configuration;

        public ApiControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            this.configuration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            var versao = GetHeaderVersao(request);

            var novasRotas = routeData
                .GetSubRoutes()
                .SelectMany(GetActionDescritor, (routeDt, actionDescritor) => new { routeDt, actionDescritor.ControllerDescriptor })
                .Where(dupla => dupla.ControllerDescriptor.ControllerName.EndsWith(versao))
                .ToList();

            routeData.Values["MS_SubRoutes"] = novasRotas.Select(dupla => dupla.routeDt).ToArray();
            return novasRotas.Select(dupla => dupla.ControllerDescriptor).FirstOrDefault();
        }

        HttpActionDescriptor[] GetActionDescritor(IHttpRouteData route)
        {
            return (HttpActionDescriptor[])route.Route.DataTokens["actions"];
        }

        string GetMediaTypeHeaderVersao(HttpRequestMessage request)
        {
            var mediaType = request.Headers.Accept;
            var regex = new Regex(@"application/vnd\.agendadecontatos\.[a-z]+\.v(?<versao>\d+)\+json");

            foreach (var mime in mediaType)
            {
                var match = regex.Match(mime.MediaType);
                if (match.Success)
                    return match.Groups["versao"].Value;
            }
            return "1";
        }

        string GetHeaderVersao(HttpRequestMessage request)
        {
            const string HEADER_NAME = "X-AgendaDeContatos-Versao";

            if (request.Headers.Contains(HEADER_NAME))
                return "V" + (request.Headers.GetValues(HEADER_NAME).FirstOrDefault() ?? "1");
            return "V1";
        }

        string GetAcceptHeaderVersao(HttpRequestMessage request)
        {
            var version = request.Headers.Accept.ToString().Split(';').FirstOrDefault(x => x.Contains("version"));
            if (!string.IsNullOrWhiteSpace(version))
                return version.Trim().Split('=')[1];
            return "1";
        }

        private object GetVersao(HttpRequestMessage request)
        {
            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);

            var versao = query["v"];
            return !string.IsNullOrWhiteSpace(versao)
                       ? versao
                       : "1";
        }
    }
}