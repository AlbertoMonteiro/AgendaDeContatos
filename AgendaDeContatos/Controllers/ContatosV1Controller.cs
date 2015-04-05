using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using AgendaDeContatos.Core.Modelos;
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
        public IHttpActionResult Get(int page = 0)
        {
            var contatos = contatosRepositorio
                .Todos()
                .Skip(page * 2)
                .Take(2);

#if NCRUNCH
            var contatosViewModel = Mapper.Map<IList<ContatoViewModel>>(contatos).ToList(); 
#else
            var contatosViewModel = contatos.Project().To<ContatoViewModel>().ToList();
#endif
            foreach (var contatoViewModel in contatosViewModel)
                contatoViewModel.PreencherUrl(Request, new { id = contatoViewModel.Id });
            return Ok(contatosViewModel);
        }

        [Route("{id}", Name = "ContatosApi")]
        public IHttpActionResult GetContatos(int id)
        {
            var contatoViewModel = Mapper.Map<ContatoViewModel>(contatosRepositorio.PorId(id));
            contatoViewModel.PreencherUrl(Request, new { id = contatoViewModel.Id });
            return Ok(contatoViewModel);
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]ContatoViewModel contatoViewModel)
        {
            if (ModelState.IsValid)
            {
                var contato = Mapper.Map<Contato>(contatoViewModel);

                contatosRepositorio.Incluir(contato);

                var location = Url.Link("ContatosApi", new { id = contato.Id });
                return Created(location, contato);
            }
            return BadRequest(ModelState);
        }

        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody]ContatoViewModel contatoViewModel)
        {
            if (ModelState.IsValid)
            {
                var contato = Mapper.Map<Contato>(contatoViewModel);
                contato.Id = id;
                contatosRepositorio.Atualizar(contato);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            var contato = contatosRepositorio.PorId(id);

            if (contato)
            {
                contatosRepositorio.Deletar(contato);
                return Ok();
            }
            return NotFound();
        }
    }
}
