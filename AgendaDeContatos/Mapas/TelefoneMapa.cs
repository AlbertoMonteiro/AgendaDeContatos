using AgendaDeContatos.Core.Modelos;
using AgendaDeContatos.Models;
using AutoMapper;

namespace AgendaDeContatos.Mapas
{
    public class TelefoneMapa : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Telefone, TelefoneViewModel>();
            Mapper.CreateMap<TelefoneViewModel, Telefone>();
        }
    }
}