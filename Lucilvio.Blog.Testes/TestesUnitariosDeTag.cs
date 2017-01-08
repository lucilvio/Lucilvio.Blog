using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucilvio.Blog.Web;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitariosDeTag
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoCriaTagSemNome()
        {
            new Tag("");
        }

        [TestMethod]
        public void TagTemNome()
        {
            var tag = new Tag("Foo");

            Assert.AreEqual("Foo", tag.Nome);
        }

        [TestMethod]
        public void SaoIguais()
        {
            Assert.AreEqual(new Tag("Foo"), new Tag("Foo"));
        }
    }
}
