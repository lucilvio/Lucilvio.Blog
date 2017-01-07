using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDePostDaListaDeGestao
    {
        public ModeloDePostDaListaDeGestao()
        {
        }

        public ModeloDePostDaListaDeGestao(Post post)
        {
            this.Id = post.Id;
            this.Titulo = post.Titulo;
            this.DataDoCadastro = post.DataDoCadastro.ToShortDateString();
            this.PermiteComentarios = post.PermiteComentarios;
        }

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string DataDoCadastro { get; set; }
        public bool PermiteComentarios { get; private set; }
    }
}