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
            return this._unidadeDeTrabalho.Lista<Usuario>().FirstOrDefault(u => u.Login == login && u.Senha == senha);
        }
    }
}