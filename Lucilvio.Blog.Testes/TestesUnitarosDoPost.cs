using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitarosDoPost
    {
        private Post _post;

        [TestInitialize]
        public void Iniciar()
        {
            this._post = new Post("Foo", "Foo Bar");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AsseguraQueNaoEhPossivelCriarUmPostSemTitulo()
        {
            new Post("", "Foo");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AsseguraQueNaoEhPossivelCriarUmPostSemTexto()
        {
            new Post("Foo", "");
        }

        [TestMethod]
        public void PostTemTitulo()
        {
            Assert.AreEqual("Foo", this._post.Titulo);
        }

        [TestMethod]
        public void PostTemConteudo()
        {
            Assert.AreEqual("Foo Bar", this._post.Conteudo);
        }

        [TestMethod]
        public void RegistraDataDaCriacaoDoPost()
        {
            var mascara = "dd/MM/yyyy hh:mm";
            var cultura = new CultureInfo("pt-BR");

            Assert.AreEqual(DateTime.Now.ToString(mascara, cultura), this._post.DataDoCadastro.ToString(mascara, cultura));
        }

        [TestMethod]
        public void AlteraDadosDoPost()
        {
            this._post.AlterarDados("Foo Bar Alterado", "Conteúdo do post alterado");

            Assert.AreEqual("Foo Bar Alterado", this._post.Titulo);
            Assert.AreEqual("Conteúdo do post alterado", this._post.Conteudo);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AsseguraQueOTituloNaoSejaVazioNaEdicao()
        {
            this._post.AlterarDados("", "Conteúdo do post alterado");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AsseguraQueOConteudoNaoSejaVazioNaEdicao()
        {
            this._post.AlterarDados("Foo Bar", "");
        }
    }
}
