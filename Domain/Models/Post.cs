namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public Post(int id, string title, string body)
    {
        Id = id;
        Title = title;
        Body = body;
    }
}