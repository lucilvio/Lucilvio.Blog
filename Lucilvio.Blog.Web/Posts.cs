using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class Posts
    {
        private static IList<Post> _posts;

        static Posts()
        {
            _posts = new List<Post>();
        }

        public void Adicionar(Post post)
        {
            _posts.Add(post);
        }

        public IEnumerable<Post> Listar()
        {
            return _posts.ToList();
        }
    }
}