using System.Data.Entity;
using System.Linq;

namespace AgendaDeContatos.Infra.Repositorios
{
    public abstract class Repositorio<T> : IRepositorio<T>
        where T : class
    {
        readonly IUnitOfWork<AgendaDeContatosDbContext> unitOfWork;
        readonly DbSet<T> dbSet;

        protected Repositorio(IUnitOfWork<AgendaDeContatosDbContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            dbSet = unitOfWork.Context.Set<T>();
        }

        public IQueryable<T> Todos()
        {
            return dbSet;
        }

        public T PorId(long id)
        {
            return dbSet.Find(id);
        }

        public void Incluir(T entidade)
        {
            dbSet.Add(entidade);
            unitOfWork.Save();
        }

        public void Atualizar(T entidade)
        {
            var entry = unitOfWork.Context.Entry(entidade);
            entry.State = EntityState.Modified;
            unitOfWork.Save();
        }

        public void Deletar(T entidade)
        {
            dbSet.Remove(entidade);
            unitOfWork.Save();
        }

        public void Deletar(long id)
        {
            dbSet.Remove(PorId(id));
            unitOfWork.Save();
        }
    }
}