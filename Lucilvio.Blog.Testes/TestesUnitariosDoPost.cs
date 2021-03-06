﻿using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            this._usuario = new Usuario("Foo", "Bar", false);
            this._post = new Post("Foo", "Foo Bar", true, this._usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoEhPossivelCriarUmPostSemTitulo()
        {
            new Post("", "Foo", true, this._usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoEhPossivelCriarUmPostSemTexto()
        {
            new Post("Foo", "", true, this._usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoEhPossivelCriarUmPostSemUsuario()
        {
            new Post("Foo", "Bar", true, null);
        }

        [TestMethod]
        public void CriaPostComTags()
        {
            var post = new Post("Foo", "Bar", true, new Usuario("Foo", "Bar", false), new List<Tag> { new Tag("Foo") });

            Assert.AreEqual(1, post.Tags.Count());
        }

        [TestMethod]
        public void CriaPostSemTags()
        {
            var post = new Post("Foo", "Bar", true, new Usuario("Foo", "Bar", false));

            Assert.AreEqual(0, post.Tags.Count());
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
        public void PostTemTags()
        {
            Assert.IsNotNull(this._post.Tags);
        }

        [TestMethod]
        public void PostPermiteComentarios()
        {
            Assert.IsTrue(this._post.PermiteComentarios);
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
            var usuario = new Usuario("Foo", "Bar", false);
            var post = new Post("niono", "nionon", true, usuario);

            Assert.IsTrue(usuario.Posts.Contains(post));
        }

        [TestMethod]
        public void AdicionaTagsAoPost()
        {
            this._post.AdicionarTags(new Tag("Foo"), new Tag("Bar"));

            Assert.AreEqual(2, this._post.Tags.Count());
        }

        [TestMethod]
        public void NaoAdicionaTagsQuandoInformaNulo()
        {
            this._post.AdicionarTags(null);

            Assert.AreEqual(0, this._post.Tags.Count());
        }

        [TestMethod]
        public void RemoveTagsDoPost()
        {
            this._post.AdicionarTags(new Tag("Foo"), new Tag("Bar"));
            this._post.RemoverTags("Foo", "Bar");

            Assert.AreEqual(0, this._post.Tags.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoAdicionaDuasTagsIguaisAoPost()
        {
            this._post.AdicionarTags(new Tag("Foo"), new Tag("Foo"));
        }

        [TestMethod]
        public void AlteraDadosDoPost()
        {
            this._post.AlterarDados("Foo Bar Alterado", "Conteúdo do post alterado", true, this._usuario);

            Assert.AreEqual("Foo Bar Alterado", this._post.Titulo);
            Assert.AreEqual("Conteúdo do post alterado", this._post.Conteudo);
            Assert.IsTrue(this._post.PermiteComentarios);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TituloNaoPodeSerVazioNaEdicao()
        {
            this._post.AlterarDados("", "Conteúdo do post alterado", true, this._usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConteudoNaoPodeSerVazioNaEdicao()
        {
            this._post.AlterarDados("Foo Bar", "", true, this._usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoEditaOPostCasoOUsuarioInformadoNaoSejaOMesmoQueCriouOPost()
        {
            this._post.AlterarDados("Foo Bar", "Bar", true, new Usuario("Foo 2", "Bar 2", false));
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
        public void NaoAdicionaComentarioQuandoInformaNulo()
        {
            this._post.AdicionarComentario(null);

            Assert.AreEqual(0, this._post.Comentarios.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoAdicionaComentarioQuandoPostEstaMarcadoParaNaoPermitirComentarios()
        {
            var post = new Post("Foo", "Bar", false, new Usuario("Foo", "Bar", false));
            post.AdicionarComentario(new Comentario("Comentário de teste"));
        }

        [TestMethod]
        public void RecuperaQuantidadeDePosts()
        {
            this._post.AdicionarComentario(new Comentario("Foo Bar"));

            Assert.AreEqual(1, this._post.QuantidadeDeComentarios);
        }
    }
}
