using System;
using System.Collections.Generic;

namespace AgendaDeContatos.Core.Modelos
{
    public class Contato : EntidadeBase
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime? Nascimento { get; set; }

        public virtual ICollection<Telefone> Telefones { get; set; }

        public string Logradouro { get; set; }

        public int Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Cep { get; set; }
    }
}
