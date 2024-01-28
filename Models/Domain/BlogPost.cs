namespace CodePulse.API.Models.Domain
{
    public class BlogPost
    {
        public Guid id { get; set; }
        public string Title { get; set; }
        public string shortDescription { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsVisible { get; set; }
        public string Author { get; set; }

        // one blogpost can have multiple category

        public ICollection<Category> Categories { get; set; }

    }
}