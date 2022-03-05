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
    public class TagCouponPivotsController : ControllerBase
    {
        private readonly DataContext _context;

        public TagCouponPivotsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TagCouponPivots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagCouponPivot>>> GetTagCouponPivot()
        {
            return await _context.TagCouponPivot.ToListAsync();
        }

        // GET: api/TagCouponPivots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagCouponPivot>> GetTagCouponPivot(int id)
        {
            var tagCouponPivot = await _context.TagCouponPivot.FindAsync(id);

            if (tagCouponPivot == null)
            {
                return NotFound();
            }

            return tagCouponPivot;
        }

        // PUT: api/TagCouponPivots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTagCouponPivot(int id, TagCouponPivot tagCouponPivot)
        {
            if (id != tagCouponPivot.Id)
            {
                return BadRequest();
            }

            _context.Entry(tagCouponPivot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagCouponPivotExists(id))
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

        // POST: api/TagCouponPivots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagCouponPivot>> PostTagCouponPivot(TagCouponPivot tagCouponPivot)
        {
            _context.TagCouponPivot.Add(tagCouponPivot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTagCouponPivot", new { id = tagCouponPivot.Id }, tagCouponPivot);
        }

        // DELETE: api/TagCouponPivots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagCouponPivot(int id)
        {
            var tagCouponPivot = await _context.TagCouponPivot.FindAsync(id);
            if (tagCouponPivot == null)
            {
                return NotFound();
            }

            _context.TagCouponPivot.Remove(tagCouponPivot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagCouponPivotExists(int id)
        {
            return _context.TagCouponPivot.Any(e => e.Id == id);
        }
    }
}
