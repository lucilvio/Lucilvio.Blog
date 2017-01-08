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
            var admin = context.Set<Usuario>().FirstOrDefault(u => u.Login == "admin");

            if (admin == null)
            {
                context.Set<Usuario>().Add(new Usuario("admin", "admin", true));
                
            }

            var tags = context.Set<Tag>().ToList();

            var tagFoo = new Tag("Foo");
            var tagBar = new Tag("Bar");

            if (!tags.Contains(tagFoo))
                context.Set<Tag>().Add(tagFoo);

            if (!tags.Contains(tagBar))
                context.Set<Tag>().Add(tagBar);

            context.SaveChanges();
        }
    }
}
