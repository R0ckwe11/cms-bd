using cms_bd.Models;

namespace cms_bd.DTOs;

public class CouponDTO
{
    public int ID { get; set; }
    public string Image { get; set; }
    public IEnumerable<TagDTO> Tags { get; set; }

    public CouponDTO(Coupon coupons, ImageMetadata image, IEnumerable<TagDTO> tags)
    {
        ID = coupons.ID;
        Image = "https://localhost:5001/images/" + image.FileName;
        Tags = tags;
    }
}