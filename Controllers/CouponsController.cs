#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms_bd.Data;
using cms_bd.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace cms_bd.Controllers
{
    [Route("api")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly DataContext _context;

        public CouponsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/coupons
        [HttpGet("coupons-page")]
        public async Task<ActionResult<IEnumerable<CouponsPageDTO>>> GetCouponsPage()
        {
            var coupons = await _context.Coupons
                .Where(t => t.IsVisible == 1 && t.IsArchived == 0)
                .OrderBy(t => t.Order)
                .ToListAsync();

            var couponsWithTags = new List<CouponDTO>();
            foreach (var coupon in coupons)
            {
                var image = await _context.ImageMetadata
                    .FirstOrDefaultAsync(t => t.ID == coupon.ImageID);

                var tagCouponPivots = await _context.TagCouponPivot
                    .Where(t => t.CouponID == coupon.ID)
                    .ToListAsync();

                var tags = new List<TagDTO>();
                foreach (var tagCouponPivot in tagCouponPivots)
                {
                    var tag = await _context.Tags
                        .Where(t => t.ID == tagCouponPivot.TagID)
                        .FirstOrDefaultAsync();
                    tags.Add(new TagDTO(tag));
                }

                couponsWithTags.Add(new CouponDTO(coupon, image, tags));
            }

            var temp = await _context.Tags
                .Where(t => t.IsArchived == 0)
                .ToListAsync();

            var allTags = temp.Select(t => new TagDTO(t)).ToList();

            return Ok(new CouponsPageDTO(couponsWithTags, allTags));
        }

        // GET: api/coupon/5
        [HttpGet("coupon/{id}")]
        public async Task<ActionResult<CouponDetailsDTO>> GetCoupon(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);

            if (coupon == null)
            {
                return NotFound();
            }

            var image = await _context.ImageMetadata
                .Where(t => t.ID == coupon.ImageID)
                .FirstOrDefaultAsync();

            return Ok(new CouponDetailsDTO(coupon, image));
        }
    }
}
