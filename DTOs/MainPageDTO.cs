using cms_bd.Controllers;
using cms_bd.Models;

namespace cms_bd.DTOs;

public class MainPageDTO
{
    public string BackgroundImage { get; set; }
    public string BackgroundColor { get; set; }
    public string ContentTitle { get; set; }
    public IEnumerable<ActivePostDTO>? ActivePosts { get; set; }
    public IEnumerable<MenuPostDTO>? MenuPosts { get; set; }

    public MainPageDTO(Config backgroundImage, Config backgroundColor, Config contentTitle, IEnumerable<ActivePostsWithImages> activePosts, IEnumerable<Post> menuPosts)
    {
        BackgroundImage = "https://localhost:5001/images/" + backgroundImage.Value;
        BackgroundColor = backgroundColor.Value;
        ContentTitle = contentTitle.Value;
        ActivePosts = activePosts.Select(activePosts => new ActivePostDTO(activePosts.ID, activePosts.Image));
        MenuPosts = menuPosts.Select(menuPost => new MenuPostDTO(menuPost.ID, menuPost.Title, menuPost.Icon));
    }
}