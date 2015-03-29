using System.Collections.Generic;
using System.Linq;
using AgendaDeContatos.Controllers;
using AgendaDeContatos.Core.Modelos;
using AgendaDeContatos.Infra.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace AgendaDeContatos.Tests.Controllers
{
    [TestClass]
    public class ContatosControllerTest
    {
        [TestMethod]
        public void Get()
        {
            var contatosStub = new List<Contato>
            {
                new Contato { Id = 1 },
                new Contato { Id = 2 }
            };

            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            contatosRepositorio.Todos().Returns(contatosStub.AsQueryable());
            var controller = new ContatosController(contatosRepositorio);

            // Act
            IEnumerable<Contato> contatos = controller.Get();

            // Assert
            contatosRepositorio.Received().Todos();
            Assert.IsNotNull(contatos);
            Assert.AreEqual(2, contatos.Count());
            Assert.AreEqual(1, contatos.ElementAt(0).Id);
            Assert.AreEqual(2, contatos.ElementAt(1).Id);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            ContatosController controller = new ContatosController(contatosRepositorio);

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            ContatosController controller = new ContatosController(contatosRepositorio);

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            ContatosController controller = new ContatosController(contatosRepositorio);

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            ContatosController controller = new ContatosController(contatosRepositorio);

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
