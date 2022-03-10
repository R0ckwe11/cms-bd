using cms_bd.Models;

namespace cms_bd.DTOs;

public class CouponDetailsDTO
{
    public string? Name { get; set; }
    public int ImageID { get; set; }
    public string? Description { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }

    public CouponDetailsDTO(Coupon coupon)
    {
        Name = coupon.Name;
        ImageID = coupon.ImageID;
        Description = coupon.Description;
        ValidFrom = coupon.ValidFrom;
        ValidTo = coupon.ValidTo;
    }
}