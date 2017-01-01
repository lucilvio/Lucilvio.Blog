using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class Usuario
    {
        private Usuario()
        {
        }

        public Usuario(string login, string senha)
        {
            if (string.IsNullOrEmpty(login))
                throw new InvalidOperationException("O login do usuário não foi informado");

            if (string.IsNullOrEmpty(senha))
                throw new InvalidOperationException("A senha do usuário não foi informada");

            this.Login = login;
            this.Senha = senha;
        }

        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
    }
}