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
            this.Tags = new List<string>();
        }

        public ModeloDePostDaLista(Post post, Usuario usuarioLogado) : this()
        {
            if (post == null)
                return;

            this.Id = post.Id;
            this.Titulo = post.Titulo;
            this.Conteudo = this.ResumirConteudo(post.Conteudo);
            this.DataDoCadastro = post.DataDoCadastro.ToShortDateString();
            this.NomeDoAutor = post.NomeDoAutor;

            post.Tags.ToList().ForEach(t => this.Tags.Add(t.Nome));

            this.QuantidadeDeComentarios = post.PermiteComentarios ? post.QuantidadeDeComentarios.ToString() : "0";
        }

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string DataDoCadastro { get; set; }
        public string QuantidadeDeComentarios { get; private set; }
        public string NomeDoAutor { get; private set; }
        public IList<string> Tags { get; set; }

        private string ResumirConteudo(string conteudo)
        {
            var tamanhoDoConteudo = conteudo.Length > 200 ? 200 : conteudo.Length;
            return conteudo.Substring(0, tamanhoDoConteudo);
        }
    }
}