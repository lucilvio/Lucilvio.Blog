using System;

namespace Lucilvio.Blog.Web
{
    public class Comentario
    {
        private Comentario()
        {
        }

        public Comentario(string conteudo)
        {
            if (string.IsNullOrEmpty(conteudo))
                throw new InvalidOperationException("Não é possível criar comentário vazio");

            this.Conteudo = conteudo;
            this.Data = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Conteudo { get; private set; }
        public DateTime Data { get; private set; }
        public Post Post { get; private set; }
    }
}