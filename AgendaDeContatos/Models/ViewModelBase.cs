using System.Net.Http;
using System.Web.Http.Routing;

namespace AgendaDeContatos.Models
{
    public abstract class ViewModelBase
    {
        public string Url { get; private set; }

        public abstract string NomeRota();

        public virtual void PreencherUrl(HttpRequestMessage request, object routeParamenters)
        {
            var urlHelper = new UrlHelper(request);
            Url = urlHelper.Link(NomeRota(), routeParamenters);
        }
    }
}