using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using AgendaDeContatos.Core.Modelos;
using AgendaDeContatos.Infra.Repositorios;

namespace AgendaDeContatos.Controllers
{
    public class ContatosController : ApiController
    {
        readonly IContatosRepositorio contatosRepositorio;

        public ContatosController(IContatosRepositorio contatosRepositorio)
        {
            this.contatosRepositorio = contatosRepositorio;
        }

        // GET api/values
        public IEnumerable<Contato> Get()
        {
            return contatosRepositorio.Todos();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
