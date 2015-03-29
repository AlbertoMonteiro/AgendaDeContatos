using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace AgendaDeContatos.Models
{
    public class ContatoViewModel : ViewModelBase
    {
        public long Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Nascimento { get; set; }

        [Required]
        public ICollection<TelefoneViewModel> Telefones { get; set; }

        public override void PreencherUrl(HttpRequestMessage request, object routeParamenters)
        {
            base.PreencherUrl(request, routeParamenters);

            foreach (var telefoneViewModel in Telefones)
                telefoneViewModel.PreencherUrl(request, new { contatoId = Id, id = telefoneViewModel.Id });
        }

        public override string NomeRota()
        {
            return "DefaultApi";
        }
    }
}