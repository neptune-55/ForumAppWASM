namespace Shared.DTOs;

public class PostBasicDto
{
    public int Id { get; set; }
    public int PosterId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public PostBasicDto(int posterId, string title, string body)
    {
        Id = -1;
        PosterId = posterId;
        Title = title;
        Body = body;
    }
}