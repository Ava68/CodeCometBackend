using Microsoft.EntityFrameworkCore;
using CodePulse.API.Models.Domain;

namespace CodePulse.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            
        }
        // DbSet class repersents collection of entities in a relational database
        public DbSet<BlogPost> BlogPosts {get; set;}
        public DbSet<Category> Categories  {get; set;}

    }
}