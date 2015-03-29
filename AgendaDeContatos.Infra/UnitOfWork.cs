using System.Data.Entity;

namespace AgendaDeContatos.Infra
{
    public class UnitOfWork<T> : IUnitOfWork<T>
        where T : DbContext
    {
        public UnitOfWork(T dbContext)
        {
            Context = dbContext;
        }

        public T Context { get; private set; }


        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
