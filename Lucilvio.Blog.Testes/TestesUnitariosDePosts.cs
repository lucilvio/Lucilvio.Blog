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
    public class TestesUnitariosDePosts
    {
        [TestMethod]
        public void AdicionarNovoPost()
        {
            var posts = new Posts();
            posts.Adicionar(new Post("Foo", "Foo bar"));

            Assert.AreEqual(1, posts.Listar().Count());
        }
    }
}
