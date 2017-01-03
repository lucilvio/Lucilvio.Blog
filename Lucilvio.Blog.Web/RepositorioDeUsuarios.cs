using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class RepositorioDeUsuarios
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;

        public RepositorioDeUsuarios(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
        }

        public void Adicionar(Usuario usuario)
        {
            this._unidadeDeTrabalho.Lista<Usuario>().Add(usuario);
            this._unidadeDeTrabalho.Persistir();
        }

        public Usuario PegarPorLoginESenha(string login, string senha)
        {
            return this._unidadeDeTrabalho.Lista<Usuario>().Include(nameof(Usuario.Posts)).FirstOrDefault(u => u.Login == login && u.Senha == senha);
        }

        public object Listar()
        {
            return this._unidadeDeTrabalho.Lista<Usuario>().Include(nameof(Usuario.Posts)).ToList();
        }

        public Usuario Pegar(int id)
        {
            return this._unidadeDeTrabalho.Lista<Usuario>().Include(nameof(Usuario.Posts)).FirstOrDefault(u => u.Id == id);
        }

        public void Alterar(int id, Usuario usuario, bool podeSeAutenticar)
        {
            var usuarioEncontrado = this.Pegar(id);
            usuarioEncontrado.AlterarDados(usuario.Login, usuario.Senha, podeSeAutenticar);

            this._unidadeDeTrabalho.Persistir();
        }
    }
}