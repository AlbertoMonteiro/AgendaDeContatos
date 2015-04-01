using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using AgendaDeContatos.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgendaDeContatos.Tests.Filters
{
    [TestClass]
    public class AutenticacaoBasicaAttributeTest
    {
        [TestMethod]
        public void QuandoReceberUmaLoginESenhaInvalidoRetorna401()
        {
            var filtro = new AutenticacaoBasicaAttribute();

            var configuration = new HttpConfiguration();
            var routeData = new HttpRouteData(new HttpRoute());
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            var httpContext = new HttpControllerContext(configuration, routeData, request);
            var descriptor = new ReflectedHttpActionDescriptor();
            var context = new HttpActionContext(httpContext, descriptor);
            var cred = string.Format("{0}:{1}", "admin", "123456");
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(cred));
            request.Headers.Authorization = new AuthenticationHeaderValue("basic", base64String);
            filtro.OnAuthorization(context);

            Assert.IsNotNull(context.Response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, context.Response.StatusCode);
        }

        [TestMethod]
        public void QuandoNaoReceberAutenticacaoRetorna401()
        {
            var filtro = new AutenticacaoBasicaAttribute();

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
        public void QuandoReceberUmaLoginESenhaValidoNaoFazNada()
        {
            var filtro = new AutenticacaoBasicaAttribute();

            var configuration = new HttpConfiguration();
            var routeData = new HttpRouteData(new HttpRoute());
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            var httpContext = new HttpControllerContext(configuration, routeData, request);
            var descriptor = new ReflectedHttpActionDescriptor();
            var context = new HttpActionContext(httpContext, descriptor);
            var cred = string.Format("{0}:{1}", "admin", "adminMaster");
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(cred));
            request.Headers.Authorization = new AuthenticationHeaderValue("basic", base64String);
            filtro.OnAuthorization(context);

            Assert.IsNull(context.Response);
        }
        
    }
}