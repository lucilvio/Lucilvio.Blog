﻿using System;
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
            this.QuantidadeDePosts = post.QuantidadeDeComentarios.ToString();
            this.Comentarios = post.Comentarios.ToList();
        }

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string DataDoCadastro { get; set; }
        public string QuantidadeDePosts { get; private set; }
        public IList<Comentario> Comentarios { get; set; }
    }
}