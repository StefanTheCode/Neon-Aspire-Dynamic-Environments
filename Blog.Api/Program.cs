using Blog.Api.Database;
using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();

            var env = builder.Environment.EnvironmentName;

            var connectionString = builder.Configuration.GetConnectionString(env);

            builder.Services.AddDbContext<BlogDbContext>(options =>
                options.UseNpgsql(connectionString));

            builder.Services.AddScoped<BlogService>();

            builder.Services.AddAuthorization();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Define API Endpoints using Minimal API
            app.MapGet("/api/blog", async (BlogService blogService) =>
                await blogService.GetAllPostsAsync());

            app.MapGet("/api/blog/{id:int}", async (int id, BlogService blogService) =>
            {
                var post = await blogService.GetPostByIdAsync(id);
                return post is null ? Results.NotFound() : Results.Ok(post);
            });

            app.MapPost("/api/blog", async (BlogPost post, BlogService blogService) =>
            {
                var createdPost = await blogService.AddPostAsync(post);
                return Results.Created($"/api/blog/{createdPost.Id}", createdPost);
            });

            app.MapDelete("/api/blog/{id:int}", async (int id, BlogService blogService) =>
            {
                return await blogService.DeletePostAsync(id) ? Results.NoContent() : Results.NotFound();
            });

            app.MapDefaultEndpoints();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}