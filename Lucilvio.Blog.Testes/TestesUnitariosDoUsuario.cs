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
            this._usuario = new Usuario("Foo bar", "senha");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UsuarioNaoPodeSerCriadoSemLogin()
        {
            new Usuario("", "senha");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UsuarioNaoPodeSerCriadoSemSenha()
        {
            new Usuario("Foo", "");
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
        public void UsuarioPodeSeAutenticar()
        {
            Assert.IsTrue(this._usuario.PodeSeAutenticar);
        }

        [TestMethod]
        public void AlteraDadosDoUsuarioRetirandoPersmissaoDeAutenticacao()
        {
            this._usuario.AlterarDados("Foo bar editado", "senha editada", false);

            Assert.AreEqual("Foo bar editado", this._usuario.Login);
            Assert.AreEqual("senha editada", this._usuario.Senha);
            Assert.IsFalse(this._usuario.PodeSeAutenticar);
        }

        [TestMethod]
        public void AlteraDadosDoUsuarioConcedendoPermissaoDeAutenticacao()
        {
            this._usuario.AlterarDados("Foo bar editado", "senha editada", true);

            Assert.AreEqual("Foo bar editado", this._usuario.Login);
            Assert.AreEqual("senha editada", this._usuario.Senha);
            Assert.IsTrue(this._usuario.PodeSeAutenticar);
        }

        [TestMethod]
        public void DaPermissaoDeAutenticacao()
        {
            this._usuario.ConcederPermissaoDeAutenticacao();

            Assert.IsTrue(this._usuario.PodeSeAutenticar);
        }

        [TestMethod]
        public void RetiraPremissaoDeAutenticacao()
        {
            this._usuario.RetirarPermissaoDeAutenticacao();

            Assert.IsFalse(this._usuario.PodeSeAutenticar);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LoginEhInformadoNaAlteracaoDeDados()
        {
            this._usuario.AlterarDados("", "nova senha", true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SenhaEhInformadoNaAlteracaoDeDados()
        {
            this._usuario.AlterarDados("Foo bar editado", "", true);
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
            var novoUsuario = new Usuario("Foo 2", "Bar 2");
            var post = new Post("noinio", "Nionoo", novoUsuario);

            Assert.IsFalse(this._usuario.PodeEditarOPost(post));
        }
    }
}
