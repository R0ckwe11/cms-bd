using cms_bd.Models;

namespace cms_bd.DTOs;

public class CouponCodeDTO
{
    public string? Code { get; set; }

    public CouponCodeDTO(Coupon coupon)
    {
        Code = coupon.Code;
    }
}