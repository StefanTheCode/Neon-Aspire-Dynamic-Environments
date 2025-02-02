using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Database;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
}