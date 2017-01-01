using Lucilvio.Blog.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Lucilvio.Blog.Web
{
    public class ServicoDeAutenticacaoViaFormsAuthentication : IServicoDeAutenticacao
    {
        public void Autenticar(Usuario usuario)
        {
            if (usuario == null)
                return;

            FormsAuthentication.SetAuthCookie(usuario.Login, false);
        }
    }
}