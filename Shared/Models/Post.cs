namespace Shared.Models;

public class Post
{
    public int Id { get; set; }
    public int PosterId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}