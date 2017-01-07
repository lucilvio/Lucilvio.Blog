using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class RepositorioDePosts
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;

        public RepositorioDePosts(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
        }

        public void Adicionar(Post post)
        {
            this._unidadeDeTrabalho.Lista<Post>().Add(post);
            this._unidadeDeTrabalho.Persistir();
        }

        public IEnumerable<Post> Listar()
        {
            return this._unidadeDeTrabalho.Lista<Post>().Include(nameof(Post.Comentarios)).Include(nameof(Post.Usuario)).ToList();
        }

        public Post Pegar(int id)
        {
            return this._unidadeDeTrabalho.Lista<Post>().Include(nameof(Post.Comentarios)).Include(nameof(Post.Usuario)).FirstOrDefault(p => p.Id == id);
        }

        public void Alterar(int id, string titulo, string conteudo, bool permiteComentarios, Usuario usuario)
        {
            var postOriginal = this.Pegar(id);
            postOriginal.AlterarDados(titulo, conteudo, permiteComentarios, usuario);

            this._unidadeDeTrabalho.Persistir();
        }

        public IEnumerable<Post> ListarPorUsuario(int idDoUsuario)
        {
            return this._unidadeDeTrabalho.Lista<Post>().Include(nameof(Post.Comentarios)).Include(nameof(Post.Usuario))
                .Where(p => p.Usuario.Id == idDoUsuario).ToList();
        }
    }
}