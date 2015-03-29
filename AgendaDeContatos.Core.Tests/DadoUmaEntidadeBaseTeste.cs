using AgendaDeContatos.Core.Modelos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgendaDeContatos.Core.Tests
{
    [TestClass]
    public class DadoUmaEntidadeBaseTeste
    {
        [TestMethod]
        public void ComparandoUmEntidadeComoBooleanoRetornaTrueSeEntidadeNaoEhNulo()
        {
            var entidade = new EntidadeFake();

            Assert.IsTrue(entidade);
        }

        [TestMethod]
        public void ComparandoUmEntidadeComoBooleanoRetornaFalseSeEntidadeEhNula()
        {
            var entidade = default(EntidadeFake);

            Assert.IsFalse(entidade);
        }


        class EntidadeFake : EntidadeBase
        {

        }
    }
}
