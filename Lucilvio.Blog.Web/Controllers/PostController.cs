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
        private IServicoDeAutenticacao _servicoDeAutenticacao;
        private RepositorioDePosts _repositorioDePosts;
        private RepositorioDeUsuarios _repositorioDeUsuarios;

        public PostController(IUnidadeDeTrabalho unidadeDeTrabalho, IServicoDeAutenticacao servicoDeAutenticacao)
        {
            this._unidadeDeTrabalho = unidadeDeTrabalho;
            this._servicoDeAutenticacao = servicoDeAutenticacao;
            this._repositorioDePosts = new RepositorioDePosts(this._unidadeDeTrabalho);
            this._repositorioDeUsuarios = new RepositorioDeUsuarios(this._unidadeDeTrabalho);
        }

        [HttpGet]
        [Authorize]
        public ActionResult MeusPosts()
        {
            var usuario = this._servicoDeAutenticacao.PegarIdentificadorDoUsuarioAutenticado();
            var posts = this._repositorioDePosts.ListarPorUsuario(usuario);

            return View(new ModeloDeListaDePostsParaGestao(posts));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [CapturarErros]
        public ActionResult Cadastrar(ModeloDePost modelo)
        {
            var usuario = this._repositorioDeUsuarios.Pegar(this._servicoDeAutenticacao.PegarIdentificadorDoUsuarioAutenticado());
            this._repositorioDePosts.Adicionar(new Post(modelo.Titulo, modelo.Conteudo, modelo.PermiteComentarios, usuario));

            this.AdicionarMensagemDeSucesso("Post cadastrado com sucesso");

            return RedirectToAction(nameof(MeusPosts));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var post = new ModeloDePost(this._repositorioDePosts.Pegar(id.Value));

            return View(post);
        }

        [HttpPost]
        [Authorize]
        [CapturarErros]
        public ActionResult Editar(ModeloDePost modelo)
        {
            var usuario = this._repositorioDeUsuarios.Pegar(this._servicoDeAutenticacao.PegarIdentificadorDoUsuarioAutenticado());
            this._repositorioDePosts.Alterar(modelo.Id, modelo.Titulo, modelo.Conteudo, modelo.PermiteComentarios, usuario);

            this.AdicionarMensagemDeSucesso("Post editado com sucesso");

            return RedirectToAction(nameof(MeusPosts));
        }

        [HttpGet]
        [CapturarErros]
        public ActionResult Ver(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            var post = new ModeloDePost(this._repositorioDePosts.Pegar(id.Value));

            return View(post);
        }

        [HttpPost]
        public JsonResult Comentar(int idDoPost, string conteudo)
        {
            var repositorioDePosts = new RepositorioDePosts(this._unidadeDeTrabalho);
            var post = repositorioDePosts.Pegar(idDoPost);

            var comentario = new Comentario(conteudo);
            post.AdicionarComentario(comentario);
            this._unidadeDeTrabalho.Persistir();

            return Json(new { comentario = comentario.Conteudo, data = comentario.Data.ToShortDateString() });
        }
    }
}