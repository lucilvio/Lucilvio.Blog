using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDePost
    {
        public ModeloDePost()
        {
            this.Comentarios = new List<Comentario>();
        }

        public ModeloDePost(Post post) : this()
        {
            this.Id = post.Id;
            this.Titulo = post.Titulo;
            this.Conteudo = post.Conteudo;
            this.DataDoCadastro = post.DataDoCadastro.ToShortDateString();
            this.QuantidadeDeComentarios = post.QuantidadeDeComentarios.ToString();
            this.Comentarios = post.Comentarios.ToList();
            this.NomeDoAutor = post.NomeDoAutor;
        }

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string DataDoCadastro { get; set; }
        public string QuantidadeDeComentarios { get; private set; }
        public string NomeDoAutor { get; set; }
        public IList<Comentario> Comentarios { get; set; }
    }
}