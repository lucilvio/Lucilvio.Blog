using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDaHome
    {
        public ModeloDaHome()
        {
            this.Tags = new ModeloDeListaDeTags();
            this.Posts = new ModeloDeListaDePosts();
        }

        public ModeloDaHome(IEnumerable<Post> posts, IEnumerable<Tag> tags) : this()
        {
            this.Tags = new ModeloDeListaDeTags(tags);
            this.Posts = new ModeloDeListaDePosts(posts);
        }

        public ModeloDeListaDeTags Tags { get; set; }
        public ModeloDeListaDePosts Posts { get; private set; }
    }
}