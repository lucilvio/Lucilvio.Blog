using Lucilvio.Blog.Web.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ContextoDoBlog : DbContext, IUnidadeDeTrabalho
    {
        public ContextoDoBlog() : base(VariaveisDeAmbiente.ConnectionString("ContextoDoBlog"))
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContextoDoBlog, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<T> Lista<T>() where T : class
        {
            return base.Set<T>();
        }

        public int Persistir()
        {
            return base.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}