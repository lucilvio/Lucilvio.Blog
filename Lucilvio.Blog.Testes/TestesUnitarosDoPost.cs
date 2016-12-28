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
        public void PostTemTexto()
        {
            Assert.AreEqual("Foo Bar", this._post.Texto);
        }

        [TestMethod]
        public void RegistraDataDaCriacaoDoPost()
        {
            var mascara = "dd/MM/yyyy hh:mm";
            var cultura = new CultureInfo("pt-BR");

            Assert.AreEqual(DateTime.Now.ToString(mascara, cultura), this._post.DataDoCadastro.ToString(mascara, cultura));
        }
    }
}
