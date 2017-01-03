using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                CookieHttpOnly = true,
                LoginPath = new PathString("/Home"),
                ExpireTimeSpan = TimeSpan.FromMinutes(20)
            });
        }
    }
}