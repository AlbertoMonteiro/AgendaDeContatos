namespace AgendaDeContatos.Core.Modelos
{
    public abstract class EntidadeBase : IEntidade
    {
        public long Id { get; set; }


        public static implicit operator bool(EntidadeBase entidade)
        {
            return entidade != null;
        }
    }
}