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
    public class TestesUnitariosDoRepositorioDePosts
    {
        private IQueryable<Post> _posts;

        private Mock<IUnidadeDeTrabalho> _mockDaUnidadeDeTrabalho;
        private Mock<IDbSet<Post>> _mockDoDbSet;

        private Usuario _usuario;

        [TestInitialize]
        public void Iniciar()
        {
            this._posts = new List<Post>().AsQueryable();
            this._mockDoDbSet = new Mock<IDbSet<Post>>();

            this._mockDoDbSet.Setup(m => m.Add(It.IsAny<Post>())).Returns((Post p) =>
            {
                var lista = new List<Post>(this._posts.ToList());
                lista.Add(p);
                this._posts = new List<Post>(lista).AsQueryable();
                this._mockDoDbSet.Setup(m => m.Provider).Returns(this._posts.Provider);
                this._mockDoDbSet.Setup(m => m.Expression).Returns(this._posts.Expression);
                this._mockDoDbSet.Setup(m => m.ElementType).Returns(this._posts.ElementType);
                this._mockDoDbSet.Setup(m => m.GetEnumerator()).Returns(this._posts.GetEnumerator());
                return p;
            });

            this._mockDaUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();
            this._mockDaUnidadeDeTrabalho.Setup(m => m.Lista<Post>()).Returns(this._mockDoDbSet.Object);
            this._mockDaUnidadeDeTrabalho.Setup(m => m.Persistir()).Returns(1);

            this._usuario = new Usuario("foo", "bar", false);
        }

        [TestMethod]
        public void AdicionaNovoPost()
        {
            var repositorioDePosts = new RepositorioDePosts(this._mockDaUnidadeDeTrabalho.Object);
            repositorioDePosts.Adicionar(new Post("Foo", "Foo bar", this._usuario));

            Assert.AreEqual(1, repositorioDePosts.Listar().Count());
        }

        [TestMethod]
        public void BuscaPostPorIdentificador()
        {
            var repositorioDePosts = new RepositorioDePosts(this._mockDaUnidadeDeTrabalho.Object);
            repositorioDePosts.Adicionar(new Post("Foo", "Foo bar", this._usuario));

            Assert.IsNotNull(repositorioDePosts.Pegar(0));
        }

        [TestMethod]
        public void BuscaPostsPorUsuario()
        {
            var repositorioDePosts = new RepositorioDePosts(this._mockDaUnidadeDeTrabalho.Object);
            repositorioDePosts.Adicionar(new Post("Foo", "Foo bar", this._usuario));
            repositorioDePosts.Adicionar(new Post("Foo", "Foo bar", this._usuario));

            Assert.AreEqual(0, repositorioDePosts.ListarPorUsuario(1).Count());
            Assert.AreEqual(2, repositorioDePosts.ListarPorUsuario(0).Count());
        }

        [TestMethod]
        public void AlteraDadosDoPost()
        {
            var repositorioDePosts = new RepositorioDePosts(this._mockDaUnidadeDeTrabalho.Object);
            repositorioDePosts.Adicionar(new Post("Foo", "Foo Bar", this._usuario));

            repositorioDePosts.Alterar(0, "Foo alterado", "Foo Bar alterado", this._usuario);

            var postAlterado = repositorioDePosts.Pegar(0);

            Assert.AreEqual("Foo alterado", postAlterado.Titulo);
            Assert.AreEqual("Foo Bar alterado", postAlterado.Conteudo);
        }
    }
}
