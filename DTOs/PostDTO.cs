using cms_bd.Models;

namespace cms_bd.DTOs;

public class PostDTO
{
    public string? Title { get; set; }
    public string? Content { get; set; }

    public PostDTO(Post post)
    {
        Title = post.Title;
        Content = post.Content;
    }
}