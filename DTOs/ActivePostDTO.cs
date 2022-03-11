using cms_bd.Models;

namespace cms_bd.DTOs;

public class ActivePostDTO
{
    public int PostID { get; set; }
    public string Image { get; set; }

    public ActivePostDTO(int ID, string image)
    {
        PostID = ID;
        Image = "https://localhost:5001/images/" + image;
    }
}