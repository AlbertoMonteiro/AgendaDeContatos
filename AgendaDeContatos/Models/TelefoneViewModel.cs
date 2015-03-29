using System.ComponentModel.DataAnnotations;
using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Models
{
    public class TelefoneViewModel : ViewModelBase
    {
        public long Id { get; set; }

        [Required]
        public TipoTelefone Tipo { get; set; }

        [Required]
        public string Numero { get; set; }

        public long ContatoId { get; set; }

        public override string NomeRota()
        {
            return "ContatosTelefoneApi";
        }
    }
}