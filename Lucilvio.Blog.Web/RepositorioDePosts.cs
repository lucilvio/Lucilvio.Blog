using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class RepositorioDePosts
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;

        public RepositorioDePosts(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
        }

        public void Adicionar(Post post)
        {
            this._unidadeDeTrabalho.Lista<Post>().Add(post);
            this._unidadeDeTrabalho.Persistir();
        }

        public IEnumerable<Post> Listar()
        {
            return this._unidadeDeTrabalho.Lista<Post>().ToList();
        }
    }
}