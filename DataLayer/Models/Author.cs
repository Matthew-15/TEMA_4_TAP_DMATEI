namespace DataLayer.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Article> Articles { get; set; }
        public Article Article { get; set; }
    }
}
