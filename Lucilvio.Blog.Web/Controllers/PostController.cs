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
            repositorioDePosts.Adicionar(new Post(modelo.Titulo, modelo.Conteudo));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var repositorioDePosts = new RepositorioDePosts(this._unidadeDeTrabalho);
            var post = repositorioDePosts.Pegar(id.Value);

            return View(post);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDePost modelo)
        {
            var repositorioDePosts = new RepositorioDePosts(this._unidadeDeTrabalho);
            repositorioDePosts.Alterar(modelo.Id, new Post(modelo.Titulo, modelo.Conteudo));

            return RedirectToAction("Index", "Home");
        }
    }
}