using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ModeloDeUsuario
    {
        public ModeloDeUsuario()
        {

        }

        public ModeloDeUsuario(Usuario usuario)
        {
            if (usuario == null)
                return;

            this.Id = usuario.Id;
            this.Login = usuario.Login;
            this.Senha = usuario.Senha;
            this.PodeSeAutenticar = usuario.PodeSeAutenticar;
            this.EhAdministrador = usuario.EhAdminitrador;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoDaSenha { get; set; }
        public bool PodeSeAutenticar { get; set; }
        public bool EhAdministrador { get; set; }
    }
}