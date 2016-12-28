using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lucilvio.Blog.Web.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDePost modelo)
        {
            var posts = new Posts();
            posts.Adicionar(new Post(modelo.Titulo, modelo.Texto));
            
            return RedirectToAction("Index", "Home");
        }
    }
}