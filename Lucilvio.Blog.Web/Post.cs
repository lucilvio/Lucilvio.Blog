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
            this.Validar(titulo, conteudo, usuario);

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
        public string NomeDoAutor => this.Usuario?.Login;

        public ICollection<Comentario> Comentarios { get; private set; }
        public Usuario Usuario { get; private set; }

        
        

        public void AlterarDados(string titulo, string conteudo, Usuario usuario)
        {
            this.Validar(titulo, conteudo, usuario);

            if (!usuario.PodeEditarOPost(this))
                throw new InvalidOperationException("O usuário não pode editar este post");

            this.Titulo = titulo;
            this.Conteudo = conteudo;
        }

        public void AdicionarComentario(Comentario comentario)
        {
            this.Comentarios.Add(comentario);
        }

        private void Validar(string titulo, string conteudo, Usuario usuario)
        {
            if (string.IsNullOrEmpty(titulo))
                throw new InvalidOperationException("Título do post não informado");

            if (string.IsNullOrEmpty(conteudo))
                throw new InvalidOperationException("Conteúdo do post não informado");

            if (usuario == null)
                throw new InvalidOperationException("Usuário do post não informado");
        }

    }
}