#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms_bd.Data;
using cms_bd.DTOs;
using cms_bd.Models;

namespace cms_bd.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly DataContext _context;

        public CouponsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/coupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouponDTO>>> GetCoupons()
        {
            var coupons = await _context.Coupons
                .Where(t => t.IsVisible == 1 && t.IsArchived == 0)
                .OrderBy(t => t.Order)
                .ToListAsync();

            var couponDTOs = new List<CouponDTO>();
            foreach (var coupon in coupons)
            {
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

                couponDTOs.Add(new CouponDTO(coupon, tags));
            }

            return couponDTOs;
        }

        // GET: api/Coupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetCoupon(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);

            if (coupon == null)
            {
                return NotFound();
            }

            return coupon;
        }

        // PUT: api/Coupons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoupon(int id, Coupon coupon)
        {
            if (id != coupon.ID)
            {
                return BadRequest();
            }

            _context.Entry(coupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Coupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coupon>> PostCoupon(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoupon", new { id = coupon.ID }, coupon);
        }

        // DELETE: api/Coupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CouponExists(int id)
        {
            return _context.Coupons.Any(e => e.ID == id);
        }
    }
}
