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
            this.Tags = new Collection<Tag>();
        }

        public Post(string titulo, string conteudo, bool permiteComentarios, Usuario usuario, IEnumerable<Tag> tags = null) : this()
        {
            this.Validar(titulo, conteudo, usuario);

            this.Conteudo = conteudo;
            this.Titulo = titulo;
            this.DataDoCadastro = DateTime.Now;
            this.PermiteComentarios = permiteComentarios;

            this.Usuario = usuario;
            this.Usuario.Posts.Add(this);

            if(tags != null)
                this.AdicionarTags(tags.ToArray());
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
        public bool PermiteComentarios { get; private set; }
        public ICollection<Tag> Tags { get; private set; }

        public void AlterarDados(string titulo, string conteudo, bool permiteComentarios, Usuario usuario, IEnumerable<Tag> tags = null)
        {
            this.Validar(titulo, conteudo, usuario);

            if (!usuario.PodeEditarOPost(this))
                throw new InvalidOperationException("O usuário não pode editar este post");

            this.Titulo = titulo;
            this.Conteudo = conteudo;
            this.PermiteComentarios = permiteComentarios;

            if (tags == null)
                return;

            this.Tags = new Collection<Tag>();
            this.AdicionarTags(tags.ToArray());
        }

        public void AdicionarComentario(Comentario comentario)
        {
            if (comentario == null)
                return;

            if (!this.PermiteComentarios)
                throw new InvalidOperationException("O post está marcado para não permitir comentários");

            this.Comentarios.Add(comentario);
        }
        public void AdicionarTags(params Tag[] tags)
        {
            if (tags == null)
                return;

            foreach (var tag in tags)
            {
                if (this.Tags.Contains(tag))
                    throw new InvalidOperationException("Não é possível adicionar duas tags iguais");

                this.Tags.Add(tag);
            }
        }

        public void RemoverTags(params string[] tags)
        {
            foreach (var nome in tags)
            {
                this.Tags.Remove(new Tag(nome));
            }
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