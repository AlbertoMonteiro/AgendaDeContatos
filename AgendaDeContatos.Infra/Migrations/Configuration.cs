using System;
using System.Collections.ObjectModel;
using System.Linq;
using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Infra.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AgendaDeContatos.Infra.AgendaDeContatosDbContext>
    {
        public Configuration()
        {
#if DEBUG
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
#else
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false; 
#endif
        }

        protected override void Seed(AgendaDeContatosDbContext context)
        {
            if (!context.Contatos.Any())
            {
                context.Contatos.Add(new Contato
                {
                    Email = "alberto.monteiro@live.com",
                    Nascimento = new DateTime(1990,3,16),
                    Nome = "Alberto Monteiro",
                    Telefones = new Collection<Telefone>
                    {
                        new Telefone
                        {
                            Numero = "8989897485",
                            Tipo = TipoTelefone.Celular
                        },
                        new Telefone
                        {
                            Numero = "8939897485",
                            Tipo = TipoTelefone.Residencial
                        }
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
