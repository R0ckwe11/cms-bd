namespace cms_bd.DTOs;

public class MenuPostDTO
{
    public int PostID { get; set; }
    public string? Title { get; set; }
    public string? Icon { get; set; }

    public MenuPostDTO(int ID, string? title, string? icon)
    {
        PostID = ID;
        Title = title;
        Icon = icon;
    }
}