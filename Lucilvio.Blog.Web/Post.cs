using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lucilvio.Blog.Web
{
    public class Post
    {
        private Post()
        {
            this.Comentarios = new Collection<Comentario>();
        }

        public Post(string titulo, string conteudo, Usuario usuario) : this()
        {
            if (usuario == null)
                throw new InvalidOperationException("Não é possível criar um post sem usuário");

            this.Validar(titulo, conteudo);

            this.Conteudo = conteudo;
            this.Titulo = titulo;
            this.DataDoCadastro = DateTime.Now;

            this.Usuario = usuario;
            this.Usuario.Posts.Add(this);
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }
        public DateTime DataDoCadastro { get; private set; }

        public bool TemComentarios => this.Comentarios.Any();
        public int QuantidadeDeComentarios => this.Comentarios.Count();

        public ICollection<Comentario> Comentarios { get; private set; }
        public Usuario Usuario { get; private set; }

        public void AlterarDados(string titulo, string conteudo, Usuario usuario)
        {
            if (usuario == null || !usuario.PodeEditarOPost(this))
                throw new InvalidOperationException("O usuário não pode editar este post");

            this.Validar(titulo, conteudo);

            this.Titulo = titulo;
            this.Conteudo = conteudo;
        }

        public void AdicionarComentario(Comentario comentario)
        {
            this.Comentarios.Add(comentario);
        }

        private void Validar(string titulo, string conteudo)
        {
            if (string.IsNullOrEmpty(titulo))
                throw new InvalidOperationException("Não é possível criar um post sem título");

            if (string.IsNullOrEmpty(conteudo))
                throw new InvalidOperationException("Não é possível criar um post sem conteúdo");
        }

    }
}