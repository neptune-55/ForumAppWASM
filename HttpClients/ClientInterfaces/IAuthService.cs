﻿using System.Security.Claims;
using Shared.DTOs;
using Shared.Models;

namespace BlazorWASM.Services;

public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}