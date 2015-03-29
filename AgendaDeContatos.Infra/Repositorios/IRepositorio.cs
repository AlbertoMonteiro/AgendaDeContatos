using System.Linq;

namespace AgendaDeContatos.Infra.Repositorios
{
    public interface IRepositorio<T>
    {
        IQueryable<T> Todos();
        T PorId(long id);
        void Incluir(T entidade);
        void Atualizar(T entidade);
        void Deletar(T entidade);
        void Deletar(long id);
    }
}
