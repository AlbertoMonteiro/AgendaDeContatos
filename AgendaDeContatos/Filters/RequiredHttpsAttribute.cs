using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AgendaDeContatos.Filters
{
    public class RequiredHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
                actionContext.Response = request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}