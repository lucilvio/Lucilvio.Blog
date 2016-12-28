using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitarosDoPost
    {
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
    }
}
