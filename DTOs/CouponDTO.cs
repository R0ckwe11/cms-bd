using cms_bd.Models;

namespace cms_bd.DTOs;

public class CouponDTO
{
    public int ID { get; set; }
    public int ImageID { get; set; }
    public IEnumerable<TagDTO> Tags { get; set; }

    public CouponDTO(Coupon coupons, IEnumerable<TagDTO> tags)
    {
        ID = coupons.ID;
        ImageID = coupons.ImageID;
        Tags = tags;
    }
}