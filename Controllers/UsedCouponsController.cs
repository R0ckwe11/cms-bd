#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms_bd;
using cms_bd.Data;
using cms_bd.Models;

namespace cms_bd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsedCouponsController : ControllerBase
    {
        private readonly DataContext _context;

        public UsedCouponsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/UsedCoupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsedCoupon>>> GetUsedCoupons()
        {
            return await _context.UsedCoupons.ToListAsync();
        }

        // GET: api/UsedCoupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsedCoupon>> GetUsedCoupon(int id)
        {
            var usedCoupon = await _context.UsedCoupons.FindAsync(id);

            if (usedCoupon == null)
            {
                return NotFound();
            }

            return usedCoupon;
        }

        // PUT: api/UsedCoupons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsedCoupon(int id, UsedCoupon usedCoupon)
        {
            if (id != usedCoupon.Id)
            {
                return BadRequest();
            }

            _context.Entry(usedCoupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsedCouponExists(id))
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

        // POST: api/UsedCoupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsedCoupon>> PostUsedCoupon(UsedCoupon usedCoupon)
        {
            _context.UsedCoupons.Add(usedCoupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsedCoupon", new { id = usedCoupon.Id }, usedCoupon);
        }

        // DELETE: api/UsedCoupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsedCoupon(int id)
        {
            var usedCoupon = await _context.UsedCoupons.FindAsync(id);
            if (usedCoupon == null)
            {
                return NotFound();
            }

            _context.UsedCoupons.Remove(usedCoupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsedCouponExists(int id)
        {
            return _context.UsedCoupons.Any(e => e.Id == id);
        }
    }
}
