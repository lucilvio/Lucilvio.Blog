using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lucilvio.Blog.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;
        private RepositorioDeUsuarios _repositorioDeUsuarios;

        public UsuarioController(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
            this._repositorioDeUsuarios = new RepositorioDeUsuarios(this._unidadeDeTrabalho);
        }

        [HttpGet]
        public ActionResult Gerenciar()
        {
            var usuarios = this._repositorioDeUsuarios.Listar();

            return View(usuarios);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [CapturarErros]
        public ActionResult Cadastrar(ModeloDeUsuario usuario)
        {
            this.ConferirSenhas(usuario.Senha, usuario.ConfirmacaoDaSenha);

            this._repositorioDeUsuarios.Adicionar(new Usuario(usuario.Login, usuario.Senha));
            this.AdicionarMensagemDeSucesso("Usuário cadastrado com sucesso");

            return RedirectToAction("Gerenciar");
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Gerenciar");

            var usuario = this._repositorioDeUsuarios.Pegar(id.Value);

            return View(new ModeloDeUsuario(usuario));
        }

        [HttpPost]
        [CapturarErros]
        public ActionResult Editar(ModeloDeUsuario usuario)
        {
            this.ConferirSenhas(usuario.Senha, usuario.ConfirmacaoDaSenha);

            this._repositorioDeUsuarios.Alterar(usuario.Id, new Usuario(usuario.Login, usuario.Senha), usuario.PodeSeAutenticar);
            this.AdicionarMensagemDeSucesso("Dados do usuário alterados com sucesso");

            return RedirectToAction("Gerenciar");
        }

        private void ConferirSenhas(string senha, string confirmacaoDaSenha)
        {
            if (senha != confirmacaoDaSenha)
                throw new InvalidOperationException("As senhas não conferem");
        }
    }
}