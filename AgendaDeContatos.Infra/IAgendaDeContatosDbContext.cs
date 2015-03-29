using System.Data.Entity;
using AgendaDeContatos.Core.Modelos;

namespace AgendaDeContatos.Infra
{
    interface IAgendaDeContatosDbContext
    {
        DbSet<Contato> Contatos { get; } 
        DbSet<Telefone> Telefones { get; } 
    }
}