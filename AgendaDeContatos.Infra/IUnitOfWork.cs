using System.Data.Entity;

namespace AgendaDeContatos.Infra
{
    public interface IUnitOfWork<T>
        where T : DbContext
    {
        T Context { get; }

        void Save();
    }
}