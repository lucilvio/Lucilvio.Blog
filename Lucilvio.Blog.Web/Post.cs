using System;

namespace Lucilvio.Blog.Web
{
    public class Post
    {
        public Post(string titulo, string texto)
        {
            if (string.IsNullOrEmpty(titulo))
                throw new InvalidOperationException("Não é possível criar um post sem texto");

            if (string.IsNullOrEmpty(texto))
                throw new InvalidOperationException("Não é possível criar um post sem texto");

            this.Texto = texto;
            this.DataDoCadastro = DateTime.Now;
        }

        public string Titulo { get; }
        public string Texto { get; }
        public DateTime DataDoCadastro { get; }
    }
}