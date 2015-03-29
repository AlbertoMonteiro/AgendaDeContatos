using System;
using System.Collections.Generic;

namespace AgendaDeContatos.Core.Modelos
{
    public class Contato : EntidadeBase
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime? Nascimento { get; set; }

        public ICollection<Telefone> Telefones { get; set; }
    }
}
