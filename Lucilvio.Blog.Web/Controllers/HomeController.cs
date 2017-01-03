using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lucilvio.Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IServicoDeAutenticacao _servicoDeAutenticacao;
        
        private readonly RepositorioDeUsuarios _repositorioDeUsuarios;

        public HomeController(IUnidadeDeTrabalho unidadeDeTrabalho, IServicoDeAutenticacao servicoDeAutenticacao)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
            this._servicoDeAutenticacao = servicoDeAutenticacao;
            this._repositorioDeUsuarios = new RepositorioDeUsuarios(this._unidadeDeTrabalho);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var listaDePostsEncontrados = new RepositorioDePosts(this._unidadeDeTrabalho).Listar();

            var idDoUsuarioLogado = _servicoDeAutenticacao.PegarIdentificadorDoUsuarioAutenticado();
            var usuarioLogado = _repositorioDeUsuarios.Pegar(idDoUsuarioLogado);

            var modeloDeListaDePosts = new ModeloDeListaDePosts(listaDePostsEncontrados, usuarioLogado);

            return View(modeloDeListaDePosts);
        }
    }
}