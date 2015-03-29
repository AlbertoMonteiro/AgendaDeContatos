using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AgendaDeContatos.Core.Modelos;
using AgendaDeContatos.Infra.Migrations;

namespace AgendaDeContatos.Infra
{
    public class AgendaDeContatosDbContext : DbContext, IAgendaDeContatosDbContext
    {
        public AgendaDeContatosDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AgendaDeContatosDbContext, Configuration>());
        }

        public DbSet<Contato> Contatos { get; set; }

        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                        .Configure(c => c.HasMaxLength(100).HasColumnType("varchar"));

            modelBuilder.Configurations.AddFromAssembly(typeof(AgendaDeContatosDbContext).Assembly);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}