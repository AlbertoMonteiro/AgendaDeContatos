using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AgendaDeContatos.Filters;
using AgendaDeContatos.Infra.Repositorios;
using AgendaDeContatos.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AgendaDeContatos.Controllers
{
#if !DEBUG
    [AutenticacaoToken] 
#endif
    public class TelefonesController : ApiController
    {
        readonly ITelefonesRepositorio telefonesRepositorio;

        public TelefonesController(ITelefonesRepositorio telefonesRepositorio)
        {
            this.telefonesRepositorio = telefonesRepositorio;
        }

        // GET api/values
        public IEnumerable<TelefoneViewModel> Get(long contatoId)
        {
            var telefoneViewModels = telefonesRepositorio.DoContato(contatoId).Project().To<TelefoneViewModel>().ToList();
            foreach (var telefoneViewModel in telefoneViewModels)
                telefoneViewModel.PreencherUrl(Request, new { contatoId, id = telefoneViewModel.Id });

            return telefoneViewModels;
        }

        // GET api/values/5
        public TelefoneViewModel Get(long contatoId, int id)
        {
            var telefoneViewModel = Mapper.Map<TelefoneViewModel>(telefonesRepositorio.DoContatoPorId(contatoId, id));
            telefoneViewModel.PreencherUrl(Request, new { contatoId, id = telefoneViewModel.Id });
            return telefoneViewModel;
        }
    }
}