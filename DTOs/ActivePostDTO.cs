using cms_bd.Models;

namespace cms_bd.DTOs;

public class ActivePostDTO
{
    public int PostID { get; set; }
    public int ImageID { get; set; }

    public ActivePostDTO(int ID, int imageID)
    {
        PostID = ID;
        ImageID = imageID;
    }
}