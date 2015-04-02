using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace AgendaDeContatos.Servicos
{
    public class MyControllerSelector : DefaultHttpControllerSelector
    {
        readonly HttpConfiguration configuration;

        public MyControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            this.configuration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();

            var routeData = request.GetRouteData();

            var controllerName = (string)routeData.Values["controller"];

            HttpControllerDescriptor descriptor;

            if (controllers.TryGetValue(controllerName, out descriptor))
            {
                //var versao = GetVersao(request);
                //var versao = GetHeaderVersao(request);
                //var versao = GetAcceptHeaderVersao(request);
                var versao = GetMediaTypeHeaderVersao(request); ;

                var newControllerName = string.Format("{0}V{1}", controllerName, versao);

                HttpControllerDescriptor newDescriptor;

                return controllers.TryGetValue(newControllerName, out newDescriptor)
                           ? newDescriptor
                           : descriptor;
            }

            return null;
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
                return request.Headers.GetValues(HEADER_NAME).FirstOrDefault() ?? "1";
            return "1";
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