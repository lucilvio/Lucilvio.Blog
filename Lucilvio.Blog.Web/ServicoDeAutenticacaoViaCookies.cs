using Lucilvio.Blog.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Lucilvio.Blog.Web
{
    public class ServicoDeAutenticacaoViaCookies : IServicoDeAutenticacao
    {
        public void Autenticar(IDictionary<string, object> dadosDoUsuario)
        {
            var autenticador = HttpContext.Current.GetOwinContext().Authentication;
            autenticador.SignIn(new ClaimsIdentity(this.MontarClaims(dadosDoUsuario), "ApplicationCookie"));
        }

        public void CancelarAutenticacao()
        {
            var autenticador = HttpContext.Current.GetOwinContext().Authentication;
            autenticador.SignOut("ApplicationCookie");
        }

        public int PegarIdentificadorDoUsuarioAutenticado()
        {
            var autenticador = HttpContext.Current.GetOwinContext().Authentication;
            var claim = autenticador.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return claim != null ? int.Parse(claim.Value) : 0;
        }

        public string PegarNomeDoUsuarioAutenticado()
        {
            var autenticador = HttpContext.Current.GetOwinContext().Authentication;
            var claim = autenticador.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            return claim != null ? claim.Value : "";
        }

        public IDictionary<string, object> PegarUsuarioAutenticado()
        {
            var autenticador = HttpContext.Current.GetOwinContext().Authentication;
            return this.RecuperarDadosDasClaims(autenticador.User.Claims);
        }

        private Claim[] MontarClaims(IDictionary<string, object> dadosDoUsuario)
        {
            var claims = new List<Claim>();

            foreach (var dado in dadosDoUsuario)
            {
                var chave = dado.Key.ToLower();
                var valor = dado.Value.ToString();

                if (chave == "id")
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, valor));
                else if (chave == "nome")
                    claims.Add(new Claim(ClaimTypes.Name, valor));
                else if (chave == "email")
                    claims.Add(new Claim(ClaimTypes.Email, valor));
                else if (chave == "perfil")
                    claims.Add(new Claim(ClaimTypes.Role, valor));
                else
                    claims.Add(new Claim(chave, valor));

                
            }

            return claims.ToArray();
        }

        private IDictionary<string, object> RecuperarDadosDasClaims(IEnumerable<Claim> claims)
        {
            var dadosDoUsuario = new Dictionary<string, object>();

            foreach (var claim in claims)
            {
                dadosDoUsuario.Add(claim.Type, claim.Value);
            }

            return dadosDoUsuario;
        }
    }
}