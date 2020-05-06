namespace Services.Hateoas.Models
{
    public class Link
    {
        public Link(string rel, string href)
        {
            Rel = rel;
            Href = href;
        }

        public string Rel { get; }
        public string Href { get; }
    }
}