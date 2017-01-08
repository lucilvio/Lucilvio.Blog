using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class Tag
    {
        private Tag()
        {
            this.Posts = new Collection<Post>();
        }

        public Tag(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new InvalidOperationException("Não pode criar uma tag sem nome");

            this.Nome = nome;
        }

        public int Id { get; private set; }

        public string Nome { get; private set; }
        public ICollection<Post> Posts { get; private set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var tag = obj as Tag;

            if (obj == null)
                return false;

            return this.Nome == tag.Nome;
        }

        public override int GetHashCode()
        {
            return this.Nome.GetHashCode();
        }
    }
}