using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Blog.Web
{
    public class RepositorioDeTags
    {
        private IUnidadeDeTrabalho _unidadeDeTrabalho;

        public RepositorioDeTags(IUnidadeDeTrabalho _unidadeDeTrabalho)
        {
            this._unidadeDeTrabalho = _unidadeDeTrabalho;
        }

        public IEnumerable<Tag> Listar()
        {
            return this._unidadeDeTrabalho.Lista<Tag>().ToList();
        }
    }
}