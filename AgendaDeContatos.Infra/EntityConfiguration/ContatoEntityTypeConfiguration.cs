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
            Property(x => x.Logradouro).HasMaxLength(50).IsRequired();
            Property(x => x.Numero).IsRequired();
            Property(x => x.Bairro).HasMaxLength(20).IsRequired();
            Property(x => x.Cidade).HasMaxLength(30).IsRequired();
            Property(x => x.Estado).HasMaxLength(20).IsRequired();
            Property(x => x.Cep).HasMaxLength(8).IsRequired();
            

            HasMany(x => x.Telefones)
                .WithRequired(x => x.Contato)
                .HasForeignKey(x => x.ContatoId)
                .WillCascadeOnDelete();
        }
    }
}
