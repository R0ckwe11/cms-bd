using cms_bd.Models;

namespace cms_bd.DTOs;

public class CouponDetailsDTO
{
    public string? Name { get; set; }
    public string Image { get; set; }
    public string? Description { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }

    public CouponDetailsDTO(Coupon coupon, ImageMetadata image)
    {
        Name = coupon.Name;
        Image = "https://localhost:5001/images/" + image.FileName;
        Description = coupon.Description;
        ValidFrom = coupon.ValidFrom;
        ValidTo = coupon.ValidTo;
    }
}