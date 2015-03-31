using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using AgendaDeContatos.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgendaDeContatos.Tests.Filters
{
    [TestClass]
    public class RequiredHttpAttributeTest
    {
        [TestMethod]
        public void QuandoReceberUmaRequisicaoHttpFiltroDeveBarrarERetornarNaoAutorizado()
        {
            var filtro = new RequiredHttpsAttribute();

            var configuration = new HttpConfiguration();
            var routeData = new HttpRouteData(new HttpRoute());
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            var httpContext = new HttpControllerContext(configuration, routeData, request);
            var descriptor = new ReflectedHttpActionDescriptor();
            var context = new HttpActionContext(httpContext, descriptor);
            filtro.OnAuthorization(context);

            Assert.IsNotNull(context.Response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, context.Response.StatusCode);
        }

        [TestMethod]
        public void QuandoReceberUmaRequisicaoHttpsFiltroDeixaPassar()
        {
            var filtro = new RequiredHttpsAttribute();

            var configuration = new HttpConfiguration();
            var routeData = new HttpRouteData(new HttpRoute());
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost");
            var httpContext = new HttpControllerContext(configuration, routeData, request);
            var descriptor = new ReflectedHttpActionDescriptor();
            var context = new HttpActionContext(httpContext, descriptor);
            filtro.OnAuthorization(context);

            Assert.IsNull(context.Response);
        }
    }
}
