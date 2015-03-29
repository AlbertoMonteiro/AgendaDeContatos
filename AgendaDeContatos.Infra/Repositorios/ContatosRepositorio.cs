using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Infra.Repositorios
{
    public class ContatosRepositorio : Repositorio<Contato>, IContatosRepositorio
    {
        public ContatosRepositorio(IUnitOfWork<AgendaDeContatosDbContext> unitOfWork)
            : base(unitOfWork) { }
    }
}