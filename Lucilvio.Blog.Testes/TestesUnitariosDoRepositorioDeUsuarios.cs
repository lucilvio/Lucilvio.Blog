using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitariosDoRepositorioDeUsuarios
    {
        private IQueryable<Usuario> _usuarios;

        private Mock<IUnidadeDeTrabalho> _mockDaUnidadeDeTrabalho;
        private Mock<IDbSet<Usuario>> _mockDoDbSet;

        private RepositorioDeUsuarios _repositorioDeUsuarios;
        private Usuario _usuario;

        [TestInitialize]
        public void Iniciar()
        {
            this._usuarios = new List<Usuario>().AsQueryable();
            this._mockDoDbSet = new Mock<IDbSet<Usuario>>();

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

            this._repositorioDeUsuarios = new RepositorioDeUsuarios(this._mockDaUnidadeDeTrabalho.Object);
            this._usuario = new Usuario("Foo Bar", "Foo", false);
        }

        [TestMethod]
        public void AdicionaUsuarioNoRepositorio()
        {
            this._repositorioDeUsuarios.Adicionar(this._usuario);

            Assert.IsTrue(this._usuarios.Count() == 1);
        }

        [TestMethod]
        public void BuscaUsuarioPorIdentificador()
        {
            this._repositorioDeUsuarios.Adicionar(this._usuario);

            Assert.IsNotNull(this._repositorioDeUsuarios.Pegar(0));
        }

        [TestMethod]
        public void BuscaUsuarioPeloLoginESenha()
        {
            this._repositorioDeUsuarios.Adicionar(this._usuario);
            var usuarioEncontrado = this._repositorioDeUsuarios.PegarPorLoginESenha("Foo Bar", "Foo");

            Assert.IsNotNull(usuarioEncontrado);
        }

        [TestMethod]
        public void NaoTrazResultadoQuandoBuscaUsuarioPeloLoginESenhaErrados()
        {
            this._repositorioDeUsuarios.Adicionar(this._usuario);
            var usuarioEncontrado = this._repositorioDeUsuarios.PegarPorLoginESenha("Foo Ba", "Foo");

            Assert.IsNull(usuarioEncontrado);
        }

        [TestMethod]
        public void ListaUsuarios()
        {
            this._repositorioDeUsuarios.Adicionar(this._usuario);
            var usuarios = this._repositorioDeUsuarios.Listar();

            Assert.AreEqual(1, this._usuarios.Count());

        }

        [TestMethod]
        public void AlteraUsuario()
        {
            this._repositorioDeUsuarios.Adicionar(this._usuario);
            this._repositorioDeUsuarios.Alterar(0, "Foo bar editado", "foo editado", false, false);

            var usuarioAlterado = this._repositorioDeUsuarios.Pegar(0);

            Assert.AreEqual("Foo bar editado", usuarioAlterado.Login);
            Assert.AreEqual("foo editado", usuarioAlterado.Senha);
            Assert.IsFalse(usuarioAlterado.PodeSeAutenticar);
            Assert.IsFalse(usuarioAlterado.EhAdminitrador);
        }
    }
}
