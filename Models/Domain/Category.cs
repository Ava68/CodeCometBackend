namespace CodePulse.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        // this repersents a single category can have multiple blogposts
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}