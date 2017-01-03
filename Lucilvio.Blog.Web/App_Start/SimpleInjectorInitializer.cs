[assembly: WebActivator.PostApplicationStartMethod(typeof(Lucilvio.Blog.Web.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace Lucilvio.Blog.Web.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IUnidadeDeTrabalho, ContextoDoBlog>(Lifestyle.Scoped);
            container.Register<IServicoDeAutenticacao, ServicoDeAutenticacaoViaCookies>(Lifestyle.Singleton);
        }
    }
}