using System;

namespace Lucilvio.Blog.Web
{
    public class Post
    {
        private Post()
        {
        }

        public Post(string titulo, string conteudo)
        {
            this.Validar(titulo, conteudo);

            this.Conteudo = conteudo;
            this.Titulo = titulo;
            this.DataDoCadastro = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }
        public DateTime DataDoCadastro { get; private set; }

        public void AlterarDados(string titulo, string conteudo)
        {
            this.Validar(titulo, conteudo);

            this.Titulo = titulo;
            this.Conteudo = conteudo;
        }

        private void Validar(string titulo, string conteudo)
        {
            if (string.IsNullOrEmpty(titulo))
                throw new InvalidOperationException("Não é possível criar um post sem título");

            if (string.IsNullOrEmpty(conteudo))
                throw new InvalidOperationException("Não é possível criar um post sem conteúdo");
        }
    }
}