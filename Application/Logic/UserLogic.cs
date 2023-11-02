using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;
    
    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            Username = dto.Username,
            Password = dto.Password
        };
    
        User created = await userDao.CreateAsync(toCreate);
        return created;
    }
    
    private static void ValidateData(UserCreationDto userToCreate)
    {
        string username = userToCreate.Username;
        string password = userToCreate.Password;

        if (username.Length < 3)
        {
            throw new Exception("Username must be at least 3 characters!");
        }

        if (username.Length > 15)
        {
            throw new Exception("Username must be less than 16 characters!");
        }

        if (password.Length < 3)
        {
            throw new Exception("Password must be at least 3 characters!");
        }
        
        if (password.Length > 15)
        {
            throw new Exception("Password must be less than 16 characters!");
        }
    }
    
    public Task<IEnumerable<User>> GetAsync()
    {
        return userDao.GetAsync();
    }

    public Task<User> GetByIdAsync(int userId)
    {
        return userDao.GetByIdAsync(userId);
    }
}