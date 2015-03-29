using System.Data.Entity;
using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Infra
{
    public class AgendaDeContatosDbContext : DbContext, IAgendaDeContatosDbContext
    {
        public DbSet<Contato> Contatos { get; private set; }

        public DbSet<Telefone> Telefones { get; private set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                        .Configure(c => c.HasMaxLength(100).HasColumnType("varchar"));

            modelBuilder.Conventions.AddFromAssembly(typeof(AgendaDeContatosDbContext).Assembly);
        }
    }
}