namespace Lucilvio.Blog.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContextoDoBlog>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ContextoDoBlog context)
        {
            context.Set<Usuario>().AddOrUpdate(new Usuario("admin", "admin"));

        }
    }
}
