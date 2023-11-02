using Application.DaoInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.Id);
            id++;
        }
        post.Id = id;
        context.Posts.Add(post);
        context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();

        if (searchParameters.Id != null)
        {
            result = result.Where(t => t.Id == searchParameters.Id);
        }
        
        if (searchParameters.Username != null)
        {
            foreach (User u in context.Users)
            {
                if (string.Equals(searchParameters.Username, u.Username))
                {
                    result = result.Where(t => t.PosterId.Equals(u.Id)); // This should work because each User has a unique username, with data validation
                }
            }
        }

        if (searchParameters.Title != null)
        {
            result = result.Where(t => t.Title.Equals(searchParameters.Title));
        }

        return Task.FromResult(result);
    }
    
    public Task<Post?> GetByIdAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        Post? existng = context.Posts.FirstOrDefault(p => p.Id == id);
        if (existng == null)
        {
            throw new Exception($"Post with id {id} does not exist!");
        }

        context.Posts.Remove(existng);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}