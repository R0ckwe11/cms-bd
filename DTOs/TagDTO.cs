using cms_bd.Models;

namespace cms_bd.DTOs;

public class TagDTO
{
    public int ID { get; set; }
    public string? Name { get; set; }

    public TagDTO(Tag tag)
    {
        ID = tag.ID;
        Name = tag.Name;
    }
}