using System.Linq;
using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Infra.Repositorios
{
    public interface ITelefonesRepositorio : IRepositorio<Telefone>
    {
        IQueryable<Telefone> DoContato(long contatoId);
        Telefone DoContatoPorId(long contatoId, int id);
    }
}