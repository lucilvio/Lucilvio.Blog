using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitariosDoUsuario
    {
        private Usuario _usuario;

        [TestInitialize]
        public void Iniciar()
        {
            this._usuario = new Usuario("Foo bar", "senha", false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UsuarioNaoPodeSerCriadoSemLogin()
        {
            new Usuario("", "senha", false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UsuarioNaoPodeSerCriadoSemSenha()
        {
            new Usuario("Foo", "", false);
        }

        [TestMethod]
        public void UsuarioNaoEhCriadoComoAdministrador()
        {
            var usuario = new Usuario("Foo", "bar", false);
            Assert.IsFalse(usuario.EhAdminitrador);
        }

        [TestMethod]
        public void UsuarioEhCriadoComPermissaoDeAcesso()
        {
            var usuario = new Usuario("Foo", "bar", true);
            Assert.IsTrue(usuario.PodeSeAutenticar);
        }

        [TestMethod]
        public void UsuarioTemLogin()
        {
            Assert.AreEqual("Foo bar", this._usuario.Login);
        }

        [TestMethod]
        public void UsuarioTemSenha()
        {
            Assert.AreEqual("senha", this._usuario.Senha);
        }

        [TestMethod]
        public void UsuarioTemPosts()
        {
            Assert.IsNotNull(this._usuario.Posts);
        }
        
        [TestMethod]
        public void AlteraDadosDoUsuarioRetirandoPersmissaoDeAutenticacao()
        {
            this._usuario.AlterarDados("Foo bar editado", "senha editada", false, true);

            Assert.AreEqual("Foo bar editado", this._usuario.Login);
            Assert.AreEqual("senha editada", this._usuario.Senha);
            Assert.IsFalse(this._usuario.PodeSeAutenticar);
        }

        [TestMethod]
        public void AlteraDadosDoUsuarioConcedendoPermissaoDeAutenticacao()
        {
            this._usuario.AlterarDados("Foo bar editado", "senha editada", true, true);

            Assert.AreEqual("Foo bar editado", this._usuario.Login);
            Assert.AreEqual("senha editada", this._usuario.Senha);
            Assert.IsTrue(this._usuario.PodeSeAutenticar);
        }

        [TestMethod]
        public void AlteraDadosDoUsuarioConcedendoPermissaoDeAdministrador()
        {
            this._usuario.AlterarDados("Foo bar editado", "senha editada", true, true);

            Assert.IsTrue(this._usuario.EhAdminitrador);
        }

        [TestMethod]
        public void AlteraDadosDoUsuarioRetirandoPermissaoDeAdministrador()
        {
            this._usuario.AlterarDados("Foo bar editado", "senha editada", true, false);

            Assert.IsFalse(this._usuario.EhAdminitrador);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LoginEhInformadoNaAlteracaoDeDados()
        {
            this._usuario.AlterarDados("", "nova senha", true, true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SenhaEhInformadoNaAlteracaoDeDados()
        {
            this._usuario.AlterarDados("Foo bar editado", "", true, true);
        }

        [TestMethod]
        public void PodeEditarOPost()
        {
            var post = new Post("Ionon", "Jiomomom", this._usuario);

            Assert.IsTrue(this._usuario.PodeEditarOPost(post));
        }

        [TestMethod]
        public void NaoPodeEditarOPost()
        {
            var novoUsuario = new Usuario("Foo 2", "Bar 2", false);
            var post = new Post("noinio", "Nionoo", novoUsuario);

            Assert.IsFalse(this._usuario.PodeEditarOPost(post));
        }
    }
}
