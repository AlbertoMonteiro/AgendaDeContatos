using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgendaDeContatos.Controllers;
using AgendaDeContatos.Core.Modelos;
using AgendaDeContatos.Infra.Repositorios;
using AgendaDeContatos.Mapas;
using AgendaDeContatos.Models;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace AgendaDeContatos.Tests.Controllers
{
    [TestClass]
    public class ContatosControllerTest
    {
        public ContatosControllerTest()
        {
            Mapper.AddProfile<ContatoMapa>();
            Mapper.AddProfile<TelefoneMapa>();
        }

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
            var controller = ContatosControllerMocked(contatosRepositorio);

            // Act
            IEnumerable<ContatoViewModel> contatos = controller.Get();

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
            contatosRepositorio.PorId(1).Returns(new Contato { Id = 1 });
            var controller = ContatosControllerMocked(contatosRepositorio);

            // Act
            var contato = controller.GetContatos(1);

            // Assert
            contatosRepositorio.Received().PorId(1);
            Assert.AreEqual(1, contato.Id);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            var controller = ContatosControllerMocked(contatosRepositorio);

            // Act
            controller.Post(new ContatoViewModel { Email = "fulanodasilva@live.com" });

            // Assert
            contatosRepositorio.Received().Incluir(Arg.Is<Contato>(c => c.Email == "fulanodasilva@live.com"));
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            var controller = ContatosControllerMocked(contatosRepositorio);

            // Act
            controller.Put(5, new ContatoViewModel { Email = "fulanodasilva@live.com" });

            // Assert
            contatosRepositorio.Received().Atualizar(Arg.Is<Contato>(c => c.Id == 5 && c.Email == "fulanodasilva@live.com"));
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            contatosRepositorio.PorId(5).Returns(new Contato() { Id = 5 });
            var controller = ContatosControllerMocked(contatosRepositorio);

            // Act
            controller.Delete(5);

            // Assert
            contatosRepositorio.Received().PorId(5);
            contatosRepositorio.Received().Deletar(Arg.Is<Contato>(c => c.Id == 5));
        }

        [TestMethod]
        public void DeleteBadRequest()
        {
            // Arrange
            var contatosRepositorio = Substitute.For<IContatosRepositorio>();
            contatosRepositorio.PorId(5).Returns(default(Contato));
            var controller = ContatosControllerMocked(contatosRepositorio);

            // Act
            var delete = controller.Delete(5);

            // Assert
            contatosRepositorio.Received().PorId(5);
            Assert.IsInstanceOfType(delete, typeof(HttpResponseMessage));
            var responseMessage = (HttpResponseMessage)delete;
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }

        static ContatosController ContatosControllerMocked(IContatosRepositorio contatosRepositorio)
        {
            var controller = new ContatosController(contatosRepositorio);
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage();

            controller.Request.RequestUri = new Uri("http://localhost/api/contatos");
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            return controller;
        }
    }
}
