using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public interface IUnidadeDeTrabalho
    {
        IDbSet<T> Lista<T>() where T : class;
        int Persistir();
    }
}