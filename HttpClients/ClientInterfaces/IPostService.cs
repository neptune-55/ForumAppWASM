using System.Collections.ObjectModel;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostBasicDto dto);
    Task<ICollection<Post>> GetAsync(int? id, int? posterId, string? title, string? body);

    Task<PostBasicDto> GetByIdAsync(int id); // I know it's weird to use a creation DTO here but it already had all the attributes
}