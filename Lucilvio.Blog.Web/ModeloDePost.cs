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
            this.Tags = new List<string>();
        }

        public ModeloDePost(Post post) : this()
        {
            this.Id = post.Id;
            this.Titulo = post.Titulo;
            this.Conteudo = post.Conteudo;
            this.DataDoCadastro = post.DataDoCadastro.ToShortDateString();
            
            this.Comentarios = post.Comentarios.ToList();
            this.NomeDoAutor = post.NomeDoAutor;
            this.PermiteComentarios = post.PermiteComentarios;

            post.Tags.ToList().ForEach(t => this.Tags.Add(t.Nome));

            this.QuantidadeDeComentarios = this.PermiteComentarios ? post.QuantidadeDeComentarios.ToString() : "0";
        }

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string DataDoCadastro { get; set; }
        public string QuantidadeDeComentarios { get; private set; }
        public string NomeDoAutor { get; set; }
        public IList<Comentario> Comentarios { get; set; }
        public IList<string> Tags { get; set; }
        public bool PermiteComentarios { get; set; }
    }
}