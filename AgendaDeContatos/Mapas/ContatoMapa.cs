﻿using AgendaDeContatos.Core.Modelos;
using AgendaDeContatos.Models;
using AutoMapper;

namespace AgendaDeContatos.Mapas
{
    public class ContatoMapa : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Contato, ContatoViewModel>();
            Mapper.CreateMap<ContatoViewModel, Contato>();
        }
    }
}