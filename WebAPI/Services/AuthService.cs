using System.Collections;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Application.Logic;
using Application.LogicInterfaces;
using FileData;
using FileData.DAOs;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserLogic userLogic;
    private readonly IList<User> users;

    public AuthService(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
        this.users = new List<User>(userLogic.GetAsync().Result.ToList());
    }
    
    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }
    
    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        users.Add(user);
        
        return Task.CompletedTask;
    }
}