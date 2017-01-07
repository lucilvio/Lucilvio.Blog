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

            if (!usuarioEncontrado.PodeSeAutenticar)
            {
                base.TempData["mensagemDeErro"] = "Usuário não tem permissão de login";
                return RedirectToAction("Index", "Home");
            }

            this._servicoDeAutenticacao.Autenticar(new Dictionary<string, object> {
                { "id",usuarioEncontrado.Id },
                { "nome",usuarioEncontrado.Login },
                { "perfil", usuarioEncontrado.EhAdminitrador ? nameof(Usuario.EhAdminitrador) : "" }
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Deslogar()
        {
            this._servicoDeAutenticacao.CancelarAutenticacao();

            return RedirectToAction("Index", "Home");
        }
    }
}