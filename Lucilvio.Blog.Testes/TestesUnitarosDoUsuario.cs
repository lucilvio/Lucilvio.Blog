using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitarosDoUsuario
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UsuarioNaoPodeSerCriadoSemLogin()
        {
            new Usuario("", "Foo");
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
            var usuario = new Usuario("Foo bar", "foo");
            Assert.AreEqual("Foo bar", usuario.Login);
        }

        [TestMethod]
        public void UsuarioTemSenha()
        {
            var usuario = new Usuario("Foo bar", "foo");
            Assert.AreEqual("foo", usuario.Senha);
        }
    }
}
