using Moq;
using System.Linq;
using Lucilvio.Blog.Web;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitariosDoRepositorioDeTags
    {
        private IQueryable<Tag> _tags;

        private Mock<IUnidadeDeTrabalho> _mockDaUnidadeDeTrabalho;
        private Mock<IDbSet<Tag>> _mockDoDbSet;

        [TestInitialize]
        public void Iniciar()
        {
            this._tags = new List<Tag>()
            {
                new Tag("Foo"),
                new Tag("Bar")
            }.AsQueryable();

            this._mockDoDbSet = new Mock<IDbSet<Tag>>();
            this._mockDoDbSet.Setup(m => m.Provider).Returns(this._tags.Provider);
            this._mockDoDbSet.Setup(m => m.Expression).Returns(this._tags.Expression);
            this._mockDoDbSet.Setup(m => m.ElementType).Returns(this._tags.ElementType);
            this._mockDoDbSet.Setup(m => m.GetEnumerator()).Returns(this._tags.GetEnumerator());

            this._mockDaUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();
            this._mockDaUnidadeDeTrabalho.Setup(m => m.Lista<Tag>()).Returns(this._mockDoDbSet.Object);
            this._mockDaUnidadeDeTrabalho.Setup(m => m.Persistir()).Returns(1);
        }

        [TestMethod]
        public void BuscaTags()
        {
            var repositorioDeTags = new RepositorioDeTags(this._mockDaUnidadeDeTrabalho.Object);
            var tags = repositorioDeTags.Listar();

            Assert.AreEqual(2, tags.Count());
        }
    }
}
