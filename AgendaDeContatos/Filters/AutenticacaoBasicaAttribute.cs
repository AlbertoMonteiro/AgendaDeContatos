using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AgendaDeContatos.Filters
{
    public class AutenticacaoBasicaAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authorization = actionContext.Request.Headers.Authorization;

            if (authorization != null &&
                authorization.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(authorization.Parameter))
            {
                var rawCred = authorization.Parameter;
                var credenciais = Encoding.UTF8.GetString(Convert.FromBase64String(rawCred));
                var partes = credenciais.Split(':');
                var username = partes[0];
                var senha = partes[1];

                if (username == "admin" && senha == "adminMaster")
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);                
            }
        }
    }
}