namespace Shared.Auth;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("IsLoggedIn", a => a.RequireAuthenticatedUser());
        });
    }
}