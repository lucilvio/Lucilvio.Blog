using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lucilvio.Blog.Web
{
    public static class ControllerExtensions
    {
        public static void AdicionarMensagemDeSucesso(this ControllerBase controller, string mensagem)
        {
            controller.TempData["mensagemDeSucesso"] = mensagem;
        }

        public static void AdicionarMensagemDeErro(this ControllerBase controller, string mensagem)
        {
            controller.TempData["mensagemDeErro"] = mensagem;
        }
    }
}