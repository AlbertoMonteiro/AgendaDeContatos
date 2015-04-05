using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AgendaDeContatos.Core.Modelos;
using AgendaDeContatos.Filters;
using AgendaDeContatos.Infra.Repositorios;
using AgendaDeContatos.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AgendaDeContatos.Controllers
{
#if !DEBUG
    [AutenticacaoBasica] 
#endif
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/contatos")]
    public class ContatosV1Controller : ApiController
    {
        readonly IContatosRepositorio contatosRepositorio;

        public ContatosV1Controller(IContatosRepositorio contatosRepositorio)
        {
            this.contatosRepositorio = contatosRepositorio;
        }

        [Route("")]
        public IEnumerable<ContatoViewModel> Get(int page = 0)
        {
            var contatoViewModels = contatosRepositorio
                .Todos()
                .Project().To<ContatoViewModel>()
                .Skip(page * 2)
                .Take(2)
                .ToList();
            foreach (var contatoViewModel in contatoViewModels)
                contatoViewModel.PreencherUrl(Request, new { controller = "contatos", id = contatoViewModel.Id });
            return contatoViewModels;
        }

        [Route("{id}", Name = "ContatosApi")]
        public ContatoViewModel GetContatos(int id)
        {
            var contatoViewModel = Mapper.Map<ContatoViewModel>(contatosRepositorio.PorId(id));
            contatoViewModel.PreencherUrl(Request, new { id = contatoViewModel.Id });
            return contatoViewModel;
        }

        // POST api/values
        public object Post([FromBody]ContatoViewModel contatoViewModel)
        {
            if (ModelState.IsValid)
            {
                var contato = Mapper.Map<Contato>(contatoViewModel);

                contatosRepositorio.Incluir(contato);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        // PUT api/values/5
        public object Put(int id, [FromBody]ContatoViewModel contatoViewModel)
        {
            if (ModelState.IsValid)
            {
                var contato = Mapper.Map<Contato>(contatoViewModel);
                contato.Id = id;
                contatosRepositorio.Atualizar(contato);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        // DELETE api/values/5
        public object Delete(int id)
        {
            var contato = contatosRepositorio.PorId(id);

            if (contato)
            {
                contatosRepositorio.Deletar(contato);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contato não existe");
        }
    }
}
