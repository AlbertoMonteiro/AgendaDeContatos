using System.Net.Http;
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
                var versao = "2";

                var newControllerName = string.Format("{0}V{1}", controllerName, versao);

                HttpControllerDescriptor newDescriptor;

                return controllers.TryGetValue(newControllerName, out newDescriptor)
                           ? newDescriptor
                           : descriptor;
            }

            return null;
        }
    }
}