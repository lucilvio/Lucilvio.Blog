using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDeCadastroDePost
    {
        public ModeloDeCadastroDePost()
        {
            this.PermiteComentarios = true;
            this.Tags = new ModeloDeListaDeTags();
        }

        public ModeloDeCadastroDePost(Post post, ModeloDeListaDeTags tags) : this()
        {
            if (post != null)
            {
                this.Titulo = post.Titulo;
                this.Conteudo = post.Conteudo;

                this.PermiteComentarios = post.PermiteComentarios;
            }

            this.Tags = tags;
        }

        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public bool PermiteComentarios { get; set; }
        public ModeloDeListaDeTags Tags { get; set; }
    }
}