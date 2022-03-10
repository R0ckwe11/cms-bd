namespace cms_bd.DTOs;

public class CouponsPageDTO
{
    public IEnumerable<CouponDTO> CouponsWithTags { get; set; }
    public IEnumerable<TagDTO> AllTags { get; set; }

    public CouponsPageDTO(IEnumerable<CouponDTO> couponsWithTags, IEnumerable<TagDTO> allTags)
    {
        CouponsWithTags = couponsWithTags;
        AllTags = allTags;
    }
}