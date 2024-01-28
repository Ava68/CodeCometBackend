
namespace CodePulse.API.Models.DTO
{
    public class CreateBlogPostRequestDto
    {
        public string Title { get; set; }
        public string shortDescription { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsVisible { get; set; }
        public string Author { get; set; }
        public Guid[] Categories { get; set; }
    }
}

