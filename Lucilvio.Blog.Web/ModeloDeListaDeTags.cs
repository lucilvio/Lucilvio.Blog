using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Blog.Web
{
    public class ModeloDeListaDeTags
    {
        public ModeloDeListaDeTags()
        {
            this.Tags = new List<ModeloDeTag>();
        }

        public ModeloDeListaDeTags(IEnumerable<Tag> tags) : this()
        {
            if (tags == null)
                return;

            tags.ToList().ForEach(t => this.Tags.Add(new ModeloDeTag(t)));
        }

        public IList<Tag> TagsAtivas(IEnumerable<Tag> tagsCadastradas)
        {
            var tags = new List<Tag>();
            var tagsAtivas = this.Tags.Where(t => t.Ativa).ToList();

            foreach (var tag in tagsAtivas)
            {
                var tagEncontrada = tagsCadastradas.FirstOrDefault(t => t.Id == tag.Id && t.Nome == tag.Nome);

                if (tagEncontrada != null)
                    tags.Add(tagEncontrada);
            }

            return tags;
        }

        public IList<ModeloDeTag> Tags { get; set; }
    }
}