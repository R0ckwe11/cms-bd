using cms_bd.Models;

namespace cms_bd.DTOs;

public class MainPageDTO
{
    public string BackgroundImage { get; set; }
    public string BackgroundColor { get; set; }
    public IEnumerable<ActivePostDTO>? ActivePosts { get; set; }
    public IEnumerable<MenuPostDTO>? MenuPosts { get; set; }

    public MainPageDTO(Config backgroundImage, Config backgroundColor, IEnumerable<Post> activePosts, IEnumerable<Post> menuPosts)
    {
        BackgroundImage = backgroundImage.Value;
        BackgroundColor = backgroundColor.Value;
        ActivePosts = activePosts?.Select(activePost => new ActivePostDTO(activePost.ID, activePost.ImageID));
        MenuPosts = menuPosts?.Select(menuPost => new MenuPostDTO(menuPost.ID, menuPost.Title, menuPost.Icon));
    }
}