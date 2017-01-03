
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDeListaDePosts
    {
        public ModeloDeListaDePosts()
        {
            this.Posts = new List<ModeloDePostDaLista>();
        }

        public ModeloDeListaDePosts(IEnumerable<Post> posts, Usuario usuarioLogado) : this()
        {
            if (posts == null)
                return;

            posts.ToList().ForEach(p => this.Posts.Add(new ModeloDePostDaLista(p, usuarioLogado)));
        }

        public IList<ModeloDePostDaLista> Posts { get; set; }
    }
}