using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Shared.DTOs;
using Shared.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;
    
    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(PostBasicDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/posts", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Post>> GetAsync(int? id, int? posterId, string? title, string? body)
    {
        HttpResponseMessage response = await client.GetAsync("/posts");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync($"/posts/{id}");
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            PostBasicDto post = JsonSerializer.Deserialize<PostBasicDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return post;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception in GetByIdAsync: {e.Message}");
            throw;
        }
    }
}