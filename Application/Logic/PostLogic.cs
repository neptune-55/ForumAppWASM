using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostBasicDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.PosterId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.PosterId} was not found.");
        }

        ValidatePost(dto);
        Post post = new Post
        {
            PosterId = dto.PosterId,
            Title = dto.Title,
            Body = dto.Body
        };
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    private void ValidatePost(PostBasicDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
        {
            throw new Exception("Title cannot be empty");
        }
        
        if (dto.Title.Length > 100)
        {
            throw new Exception("Title cannot be longer than 100 characters");
        }
        
        if (string.IsNullOrEmpty(dto.Body))
        {
            throw new Exception("Body cannot be empty");
        }

        if (dto.Body.Length > 3000)
        {
            throw new Exception("Body cannot be longer than 3000 characters");
        }
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        // Converts from post to dto of post because the upper layers need the dto
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception("Post is null");
        }
        else
        {
            string title = post.Title;
            string body = post.Body;
            int posterId = post.PosterId;
            PostBasicDto dto = new PostBasicDto(posterId, title, body);
            dto.Id = id;
            return dto;
        }
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }

        await postDao.DeleteAsync(id);
    }
}