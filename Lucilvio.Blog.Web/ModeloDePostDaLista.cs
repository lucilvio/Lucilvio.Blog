using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDePostDaLista
    {
        public ModeloDePostDaLista()
        {
        }

        public ModeloDePostDaLista(Post post, Usuario usuarioLogado)
        {
            this.Id = post.Id;
            this.Titulo = post.Titulo;
            this.Conteudo = this.ResumirConteudo(post.Conteudo);
            this.DataDoCadastro = post.DataDoCadastro.ToShortDateString();
            this.QuantidadeDePosts = post.QuantidadeDeComentarios.ToString();
            this.PodeSerEditado = usuarioLogado != null && usuarioLogado.PodeEditarOPost(post);
        }

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string DataDoCadastro { get; set; }
        public string QuantidadeDePosts { get; private set; }
        public bool PodeSerEditado { get; set; }

        private string ResumirConteudo(string conteudo)
        {
            var tamanhoDoConteudo = conteudo.Length > 200 ? 200 : conteudo.Length;
            return conteudo.Substring(0, tamanhoDoConteudo);
        }
    }
}