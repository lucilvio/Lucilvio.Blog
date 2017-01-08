namespace Lucilvio.Blog.Web
{
    public class ModeloDeTag
    {
        public ModeloDeTag()
        {

        }

        public ModeloDeTag(Tag tag)
        {
            if (tag == null)
                return;

            this.Id = tag.Id;
            this.Nome = tag.Nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativa { get; set; }
    }
}