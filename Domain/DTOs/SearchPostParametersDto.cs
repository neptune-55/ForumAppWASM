namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public int? Id { get; }
    public string? Username { get; }
    public string? Title { get; }

    public SearchPostParametersDto(int? id, string? username, string? title)
    {
        Id = id;
        Username = username;
        Title = title;
    }
}