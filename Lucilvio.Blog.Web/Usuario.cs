using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class Usuario
    {
        private Usuario()
        {
            this.Posts = new Collection<Post>();
        }

        public Usuario(string login, string senha) : this()
        {
            this.Validar(login, senha);

            this.Login = login;
            this.Senha = senha;
            this.ConcederPermissaoDeAutenticacao();
        }

        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public bool PodeSeAutenticar { get; private set; }
        public ICollection<Post> Posts { get; private set; }

        public void AlterarDados(string login, string senha, bool podeSeAutenticar)
        {
            this.Validar(login, senha);

            this.Login = login;
            this.Senha = senha;

            if (podeSeAutenticar)
                this.ConcederPermissaoDeAutenticacao();
            else
                this.RetirarPermissaoDeAutenticacao();
        }

        public void ConcederPermissaoDeAutenticacao()
        {
            this.PodeSeAutenticar = true;
        }

        public void RetirarPermissaoDeAutenticacao()
        {
            this.PodeSeAutenticar = false;
        }
        public bool PodeEditarOPost(Post post)
        {
            return this.Posts.Contains(post);
        }

        private void Validar(string login, string senha)
        {
            if (string.IsNullOrEmpty(login))
                throw new InvalidOperationException("O login do usuário não foi informado");

            if (string.IsNullOrEmpty(senha))
                throw new InvalidOperationException("A senha do usuário não foi informada");
        }
    }
}