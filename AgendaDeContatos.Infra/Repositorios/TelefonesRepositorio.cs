using System.Linq;
using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Infra.Repositorios
{
    public class TelefonesRepositorio : Repositorio<Telefone>, ITelefonesRepositorio
    {
        public TelefonesRepositorio(IUnitOfWork<AgendaDeContatosDbContext> unitOfWork)
            : base(unitOfWork) { }

        public IQueryable<Telefone> DoContato(long contatoId)
        {
            return unitOfWork.Context.Telefones.Where(t => t.ContatoId == contatoId);
        }

        public Telefone DoContatoPorId(long contatoId, int id)
        {
            return unitOfWork.Context.Telefones.SingleOrDefault(t => t.ContatoId == contatoId && t.Id == id);
        }
    }
}