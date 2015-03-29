using System.Data.Entity.ModelConfiguration;
using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Infra.EntityConfiguration
{
    public class TelefoneEntityTypeConfiguration : EntityTypeConfiguration<Telefone>
    {
        public TelefoneEntityTypeConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Numero).IsRequired();
            Property(x => x.Tipo).IsRequired();
            

            HasRequired(x => x.Contato)
                .WithMany(x => x.Telefones)
                .HasForeignKey(x => x.ContatoId)
                .WillCascadeOnDelete();
        }
    }
}