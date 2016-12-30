using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitariosDeComentario
    {
        private Comentario _comentario;

        [TestInitialize]
        public void Iniciar()
        {
            this._comentario = new Comentario("Foo bar");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AsseguraQueOComentarioTemConteudo()
        {
            new Comentario("");
        }


        [TestMethod]
        public void ComentarioTemConteudo()
        {
            Assert.IsNotNull(this._comentario.Conteudo);
        }

        [TestMethod]
        public void TemData()
        {
            Assert.IsNotNull(this._comentario.Data);
        }

        [TestMethod]
        public void AsseguraQueADataDoComentarioEhIgualADataDeCriacao()
        {
            var mascara = "dd/MM/yyyy hh:mm";
            var cultura = new CultureInfo("pt-BR");

            Assert.AreEqual(DateTime.Now.ToString(mascara, cultura), this._comentario.Data.ToString(mascara, cultura));
        }

    }
}
