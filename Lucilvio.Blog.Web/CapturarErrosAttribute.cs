using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lucilvio.Blog.Web
{
    internal class CapturarErrosAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!(filterContext.Exception is InvalidOperationException))
                return;

            filterContext.Controller.AdicionarMensagemDeErro(filterContext.Exception.Message);
            filterContext.ExceptionHandled = true;

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                { "action", filterContext.Controller.ControllerContext.RouteData.Values["action"] },
                { "controller", filterContext.Controller.ControllerContext.RouteData.Values["controller"] },
                { "id", filterContext.Controller.ControllerContext.RouteData.Values["id"] }
            });
        }
    }
}