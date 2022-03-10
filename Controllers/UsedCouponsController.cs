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
        [HttpPost("used-coupons/{couponID}/{userID}")]
        public async Task<ActionResult<UsedCouponDTO>> UseCoupon(int couponID, int userID = 1)
        {
            var usedCoupon = _context.UsedCoupons.Add(new UsedCoupon()
            {
                CouponID = couponID,
                UserID = userID
            }).Entity;
            await _context.SaveChangesAsync();

            return Ok(new UsedCouponDTO(usedCoupon));
        }
    }
}
