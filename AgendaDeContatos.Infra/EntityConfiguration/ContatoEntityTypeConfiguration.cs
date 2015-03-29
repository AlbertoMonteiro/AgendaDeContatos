using System.Data.Entity.ModelConfiguration;
using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Infra.EntityConfiguration
{
    public class ContatoEntityTypeConfiguration : EntityTypeConfiguration<Contato>
    {
        public ContatoEntityTypeConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Email).IsRequired();
            Property(x => x.Nome).IsRequired();
            Property(x => x.Nascimento);
            

            HasMany(x => x.Telefones)
                .WithRequired(x => x.Contato)
                .HasForeignKey(x => x.ContatoId)
                .WillCascadeOnDelete();
        }
    }
}
