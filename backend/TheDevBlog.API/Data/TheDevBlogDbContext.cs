using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Models.Entites;

namespace TheDevBlog.API.Data
{
    public class TheDevBlogDbContext:DbContext
    {
        public TheDevBlogDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
    }
}
