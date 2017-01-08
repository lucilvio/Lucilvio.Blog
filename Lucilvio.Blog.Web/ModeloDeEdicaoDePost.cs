using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDeEdicaoDePost
    {
        public ModeloDeEdicaoDePost()
        {
            this.Tags = new ModeloDeListaDeTags();
        }

        public ModeloDeEdicaoDePost(Post post, ModeloDeListaDeTags tags) : this()
        {
            if (post != null)
            {
                this.Id = post.Id;
                this.Titulo = post.Titulo;
                this.Conteudo = post.Conteudo;

                this.PermiteComentarios = post.PermiteComentarios;

                foreach(var tag in tags.Tags)
                {
                    if(post.Tags.FirstOrDefault(t => t.Nome == tag.Nome && t.Id == tag.Id) != null)
                    {
                        tag.Ativa = true;
                    }
                }
            }

            this.Tags = tags;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public bool PermiteComentarios { get; set; }
        public ModeloDeListaDeTags Tags { get; set; }
    }
}