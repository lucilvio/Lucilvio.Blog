using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lucilvio.Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;

        public PostController(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDePost modelo)
        {
            var repositorioDePosts = new RepositorioDePosts(this._unidadeDeTrabalho);
            repositorioDePosts.Adicionar(new Post(modelo.Titulo, modelo.Texto));

            return RedirectToAction("Index", "Home");
        }
    }
}