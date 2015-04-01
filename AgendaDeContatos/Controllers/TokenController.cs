using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using AgendaDeContatos.Models;

namespace AgendaDeContatos.Controllers
{
    public class TokenController : ApiController
    {
        static List<App> apps = new List<App>
        {
            new App {AppKey = "123", Chave = "chave"}
        };

        public static List<AuthToken> tokens = new List<AuthToken>();
 
        public object Post(RequestTokenModel model)
        {
            var app = apps.First(a => a.AppKey == model.AppKey);

            var key = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(app.Chave)));
            var provider = new System.Security.Cryptography.HMACSHA256(key);
            var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(model.AppKey));
            var signature = Convert.ToBase64String(hash);

            if (signature == model.Signature)
            {
                var expiracao = DateTime.Now.AddHours(2);
                var rawTokenInfo = string.Format("{0}|{1}", app.AppKey, expiracao);
                var rawTokenBytes = Encoding.UTF8.GetBytes(rawTokenInfo);
                var token = provider.ComputeHash(rawTokenBytes);

                var authToken = new AuthToken
                {
                    Token = Convert.ToBase64String(token),
                    Expiracao = expiracao
                };
                tokens.Add(authToken);
                return Request.CreateResponse(HttpStatusCode.Created, authToken);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Sem permissão");
        }
    }
}