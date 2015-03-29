namespace AgendaDeContatos.Core.Modelos
{
    public class Telefone : EntidadeBase
    {
        public TipoTelefone Tipo { get; set; }

        public string Numero { get; set; }
    }
}