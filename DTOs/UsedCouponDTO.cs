using cms_bd.Models;

namespace cms_bd.DTOs;

public class UsedCouponDTO
{
    public DateTime UsedAt { get; set; }

    public UsedCouponDTO(UsedCoupon usedCoupon)
    {
        UsedAt = usedCoupon.UsedAt;
    }
}