namespace WebAPI.Dto
{
    public class ArticleDto
    {
        public ArticleDto(string title, string description, string url)
        {
            Title = title;
            Description = description;
            Url = url;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
