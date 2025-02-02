using Blog.Api.Database;
using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public class BlogService
{
    private readonly BlogDbContext _context;

    public BlogService(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BlogPost>> GetAllPostsAsync() =>
        await _context.BlogPosts.ToListAsync();

    public async Task<BlogPost?> GetPostByIdAsync(int id) =>
        await _context.BlogPosts.FindAsync(id);

    public async Task<BlogPost> AddPostAsync(BlogPost post)
    {
        _context.BlogPosts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post == null) return false;

        _context.BlogPosts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }
}