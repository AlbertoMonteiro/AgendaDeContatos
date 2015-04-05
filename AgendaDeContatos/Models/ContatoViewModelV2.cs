namespace AgendaDeContatos.Models
{
    public class ContatoViewModelV2 : ContatoViewModel
    {
        public string Logradouro { get; set; }

        public int Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Cep { get; set; }

        public override string NomeRota()
        {
            return "ContatosApiV2";
        }
    }
}