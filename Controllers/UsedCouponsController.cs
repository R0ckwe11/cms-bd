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
    [Route("api")]
    [ApiController]
    public class UsedCouponsController : ControllerBase
    {
        private readonly DataContext _context;

        public UsedCouponsController(DataContext context)
        {
            _context = context;
        }

        // POST: api/used-coupons/5/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("coupon-code/{id}")]
        public async Task<ActionResult<CouponCodeDTO>> UseCoupon(int id)
        {
            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(t => t.ID == id);
            if (coupon == null)
            {
                return NotFound();
            }

            var usedCoupon = _context.UsedCoupons.Add(new UsedCoupon()
            {
                CouponID = id,
                UserID = 1
            }).Entity;
            await _context.SaveChangesAsync();

            return Ok(new CouponCodeDTO(coupon));
        }
    }
}
