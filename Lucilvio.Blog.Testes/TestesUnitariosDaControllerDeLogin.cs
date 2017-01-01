﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucilvio.Blog.Web.Controllers;
using Moq;
using Lucilvio.Blog.Web;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitariosDaControllerDeLogin
    {
        private IQueryable<Usuario> _usuarios;

        private Mock<IUnidadeDeTrabalho> _mockDaUnidadeDeTrabalho;
        private Mock<IDbSet<Usuario>> _mockDoDbSet;

        private Mock<IServicoDeAutenticacao> _mockDoServicoDeAutenticacao;

        private RepositorioDeUsuarios _repositorioDeUsuarios;

        [TestInitialize]
        public void Iniciar()
        {
            this._usuarios = new List<Usuario>().AsQueryable();
            this._mockDoDbSet = new Mock<IDbSet<Usuario>>();

            this._mockDoDbSet.Setup(m => m.Provider).Returns(this._usuarios.Provider);
            this._mockDoDbSet.Setup(m => m.Expression).Returns(this._usuarios.Expression);
            this._mockDoDbSet.Setup(m => m.ElementType).Returns(this._usuarios.ElementType);
            this._mockDoDbSet.Setup(m => m.GetEnumerator()).Returns(this._usuarios.GetEnumerator());

            this._mockDoDbSet.Setup(m => m.Add(It.IsAny<Usuario>())).Returns((Usuario u) =>
            {
                this._usuarios = new List<Usuario> { u }.AsQueryable();
                this._mockDoDbSet.Setup(m => m.Provider).Returns(this._usuarios.Provider);
                this._mockDoDbSet.Setup(m => m.Expression).Returns(this._usuarios.Expression);
                this._mockDoDbSet.Setup(m => m.ElementType).Returns(this._usuarios.ElementType);
                this._mockDoDbSet.Setup(m => m.GetEnumerator()).Returns(this._usuarios.GetEnumerator());
                return u;
            });

            this._mockDaUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();
            this._mockDaUnidadeDeTrabalho.Setup(m => m.Lista<Usuario>()).Returns(this._mockDoDbSet.Object);
            this._mockDaUnidadeDeTrabalho.Setup(m => m.Persistir()).Returns(1);

            this._mockDoServicoDeAutenticacao = new Mock<IServicoDeAutenticacao>();
            this._mockDoServicoDeAutenticacao.Setup(m => m.Autenticar(It.IsAny<Usuario>())).Callback((Usuario u) => { new Usuario("Foo bar", "foo"); });

            this._repositorioDeUsuarios = new RepositorioDeUsuarios(this._mockDaUnidadeDeTrabalho.Object);
        }

        [TestMethod]
        public void FazLoginQuandoUsuarioESenhaSaoInformadosCorretamente()
        {
            this._repositorioDeUsuarios.Adicionar(new Usuario("Foo bar", "foo"));

            var controller = new LoginController(this._mockDaUnidadeDeTrabalho.Object, this._mockDoServicoDeAutenticacao.Object);
            var resultado = controller.Logar("Foo bar", "foo") as RedirectToRouteResult;

            Assert.AreEqual(null, controller.TempData["mensagemDeErro"]);
            Assert.AreEqual("Index", resultado.RouteValues["action"]);
            Assert.AreEqual("Home", resultado.RouteValues["controller"]);
        }

        [TestMethod]
        public void NaoFazLoginQuandoUsuarioESenhaSaoInformadosIncorretamente()
        {
            var controller = new LoginController(this._mockDaUnidadeDeTrabalho.Object, this._mockDoServicoDeAutenticacao.Object);
            var resultado = controller.Logar("Foo bar", "foo") as RedirectToRouteResult;

            Assert.AreEqual("Login incorreto", controller.TempData["mensagemDeErro"]);
        }
    }
}