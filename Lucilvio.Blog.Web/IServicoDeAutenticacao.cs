using System.Collections.Generic;
using System.Security.Principal;

namespace Lucilvio.Blog.Web
{
    public interface IServicoDeAutenticacao
    {
        void Autenticar(IDictionary<string, object> dadosDoUsuario);
        void CancelarAutenticacao();
        int PegarIdentificadorDoUsuarioAutenticado();
        string PegarNomeDoUsuarioAutenticado();
        IDictionary<string, object> PegarUsuarioAutenticado();
    }
}