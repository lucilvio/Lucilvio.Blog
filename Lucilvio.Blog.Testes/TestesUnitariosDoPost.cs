using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitariosDoPost
    {
        private Post _post;
        private Usuario _usuario;

        [TestInitialize]
        public void Iniciar()
        {
            this._usuario = new Usuario("Foo", "Bar");
            this._post = new Post("Foo", "Foo Bar", this._usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoEhPossivelCriarUmPostSemTitulo()
        {
            new Post("", "Foo", new Usuario("Foo", "Bar"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoEhPossivelCriarUmPostSemTexto()
        {
            new Post("Foo", "", new Usuario("Foo", "Bar"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoEhPossivelCriarUmPostSemUsuario()
        {
            new Post("Foo", "Bar", null);
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
        public void PostTemUsuario()
        {
            Assert.IsNotNull(this._post.Usuario);
        }

        [TestMethod]
        public void RegistraDataDaCriacaoDoPost()
        {
            var mascara = "dd/MM/yyyy hh:mm";
            var cultura = new CultureInfo("pt-BR");

            Assert.AreEqual(DateTime.Now.ToString(mascara, cultura), this._post.DataDoCadastro.ToString(mascara, cultura));
        }

        [TestMethod]
        public void AdicionarOPostCriadoAoUsuarioAssociado()
        {
            var usuario = new Usuario("Foo", "Bar");
            var post = new Post("niono", "nionon", usuario);

            Assert.IsTrue(usuario.Posts.Contains(post));
        }

        [TestMethod]
        public void AlteraDadosDoPost()
        {
            this._post.AlterarDados("Foo Bar Alterado", "Conteúdo do post alterado", this._usuario);

            Assert.AreEqual("Foo Bar Alterado", this._post.Titulo);
            Assert.AreEqual("Conteúdo do post alterado", this._post.Conteudo);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TituloNaoPodeSerVazioNaEdicao()
        {
            this._post.AlterarDados("", "Conteúdo do post alterado", this._usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConteudoNaoPodeSerVazioNaEdicao()
        {
            this._post.AlterarDados("Foo Bar", "", this._usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoEditaOPostCasoOUsuarioInformadoNaoSejaOMesmoQueCriouOPost()
        {
            this._post.AlterarDados("Foo Bar", "Bar", new Usuario("Foo 2", "Bar 2"));
        }

        [TestMethod]
        public void TemComentarios()
        {
            Assert.IsNotNull(this._post.Comentarios);
        }

        [TestMethod]
        public void TemNomeDoAutor()
        {
            Assert.AreEqual("Foo", this._post.NomeDoAutor);
        }

        [TestMethod]
        public void AdicionaComentarioAoPost()
        {
            this._post.AdicionarComentario(new Comentario("Foo Bar"));

            Assert.IsTrue(this._post.TemComentarios);
        }

        [TestMethod]
        public void RecuperaQuantidadeDePosts()
        {
            this._post.AdicionarComentario(new Comentario("Foo Bar"));

            Assert.AreEqual(1, this._post.QuantidadeDeComentarios);
        }
    }
}
