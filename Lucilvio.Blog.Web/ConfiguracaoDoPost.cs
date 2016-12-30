using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ConfiguracaoDoPost : EntityTypeConfiguration<Post>
    {
        public ConfiguracaoDoPost()
        {
            base.ToTable("Post");
            base.HasKey(p => p.Id);

            base.Ignore(p => p.TemComentarios);

            base.Property(p => p.Conteudo).IsMaxLength().IsRequired();
            base.Property(p => p.Titulo).HasMaxLength(255).IsRequired();
            base.Property(p => p.DataDoCadastro).IsRequired();
        }
    }
}