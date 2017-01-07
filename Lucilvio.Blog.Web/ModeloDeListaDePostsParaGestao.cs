
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDeListaDePostsParaGestao
    {
        public ModeloDeListaDePostsParaGestao()
        {
            this.Posts = new List<ModeloDePostDaListaDeGestao>();
        }

        public ModeloDeListaDePostsParaGestao(IEnumerable<Post> posts) : this()
        {
            if (posts == null)
                return;

            posts.ToList().ForEach(p => this.Posts.Add(new ModeloDePostDaListaDeGestao(p)));
        }

        public IList<ModeloDePostDaListaDeGestao> Posts { get; set; }
    }
}