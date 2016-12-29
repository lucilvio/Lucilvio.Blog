﻿using Lucilvio.Blog.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Lucilvio.Blog.Testes
{
    [TestClass]
    public class TestesUnitariosDoRepositorioDePosts
    {
        private IQueryable<Post> _posts;

        private Mock<IUnidadeDeTrabalho> _mockDaUnidadeDeTrabalho;
        private Mock<IDbSet<Post>> _mockDoDbSet;

        [TestInitialize]
        public void Iniciar()
        {
            this._posts = new List<Post>().AsQueryable();
            this._mockDoDbSet = new Mock<IDbSet<Post>>();

            this._mockDoDbSet.Setup(m => m.Add(It.IsAny<Post>())).Returns((Post p) =>
            {
                this._posts = new List<Post> { p }.AsQueryable();
                this._mockDoDbSet.Setup(m => m.Provider).Returns(this._posts.Provider);
                this._mockDoDbSet.Setup(m => m.Expression).Returns(this._posts.Expression);
                this._mockDoDbSet.Setup(m => m.ElementType).Returns(this._posts.ElementType);
                this._mockDoDbSet.Setup(m => m.GetEnumerator()).Returns(this._posts.GetEnumerator());
                return p;
            });

            this._mockDaUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();
            this._mockDaUnidadeDeTrabalho.Setup(m => m.Lista<Post>()).Returns(this._mockDoDbSet.Object);
            this._mockDaUnidadeDeTrabalho.Setup(m => m.Persistir()).Returns(1);
        }

        [TestMethod]
        public void AdicionaNovoPost()
        {
            var repositorioDePosts = new RepositorioDePosts(this._mockDaUnidadeDeTrabalho.Object);
            repositorioDePosts.Adicionar(new Post("Foo", "Foo bar"));

            Assert.AreEqual(1, repositorioDePosts.Listar().Count());
        }
    }
}
