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

            this.Comentarios = post.Comentarios.ToList();
        }

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string DataDoCadastro { get; set; }
        public IList<Comentario> Comentarios { get; set; }

        public string ConteudoResumido
        {
            get
            {
                var tamanhoDoConteudo = this.Conteudo.Length > 200 ? 200 : this.Conteudo.Length;
                return this.Conteudo.Substring(0, tamanhoDoConteudo);
            }
        }

    }
}