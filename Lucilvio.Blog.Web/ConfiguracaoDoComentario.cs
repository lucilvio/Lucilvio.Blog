using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lucilvio.Blog.Web
{
    public class ConfiguracaoDoComentario : EntityTypeConfiguration<Comentario>
    {
        public ConfiguracaoDoComentario()
        {
            base.ToTable("Comentario");
            base.HasKey(c => c.Id);
            base.Property(c => c.Conteudo).IsMaxLength().IsRequired();
            base.Property(c => c.Data).IsRequired();

            base.HasRequired(c => c.Post).WithMany(p => p.Comentarios);
        }
    }
}