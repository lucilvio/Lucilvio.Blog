using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ConfiguracaoDoUsuario : EntityTypeConfiguration<Usuario>
    {
        public ConfiguracaoDoUsuario()
        {
            base.ToTable("Usuario");
            base.HasKey(u => u.Id);

            base.Property(u => u.EhAdminitrador).IsRequired();
            base.Property(u => u.Login).HasMaxLength(255).IsRequired();
            base.Property(p => p.Senha).HasMaxLength(255).IsRequired();
        }
    }
}