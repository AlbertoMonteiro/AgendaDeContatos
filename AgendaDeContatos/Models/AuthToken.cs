using System;

namespace AgendaDeContatos.Models
{
    public class AuthToken
    {
        public string Token { get; set; }

        public DateTime Expiracao { get; set; }
    }
}