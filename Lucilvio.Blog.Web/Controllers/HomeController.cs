using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lucilvio.Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;

        public HomeController(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var listaDePostsEncontrados = new RepositorioDePosts(this._unidadeDeTrabalho).Listar();
            var modeloDeListaDePosts = new ModeloDeListaDePosts(listaDePostsEncontrados);

            return View(modeloDeListaDePosts);
        }
    }
}