using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lucilvio.Blog.Web.Controllers
{
    public class LoginController : Controller
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;
        private IServicoDeAutenticacao _servicoDeAutenticacao;

        public LoginController(IUnidadeDeTrabalho unidadeDeTrabalho, IServicoDeAutenticacao servicoDeAutenticacao)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
            this._servicoDeAutenticacao = servicoDeAutenticacao;
        }

        [HttpPost]
        public ActionResult Logar(string login, string senha)
        {
            var repositorioDeUsuarios = new RepositorioDeUsuarios(this._unidadeDeTrabalho);
            var usuarioEncontrado = repositorioDeUsuarios.PegarPorLoginESenha(login, senha);

            if (usuarioEncontrado == null)
            {
                base.TempData["mensagemDeErro"] = "Login incorreto";
                return RedirectToAction("Index", "Home");
            }

            this._servicoDeAutenticacao.Autenticar(usuarioEncontrado);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Deslogar()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}