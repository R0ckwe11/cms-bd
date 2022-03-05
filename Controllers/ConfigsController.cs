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
    public class ConfigsController : ControllerBase
    {
        private readonly DataContext _context;

        public ConfigsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Configs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Config>>> GetConfig()
        {
            return await _context.Config.ToListAsync();
        }

        // GET: api/Configs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Config>> GetConfig(int id)
        {
            var config = await _context.Config.FindAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            return config;
        }

        // PUT: api/Configs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfig(int id, Config config)
        {
            if (id != config.Id)
            {
                return BadRequest();
            }

            _context.Entry(config).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigExists(id))
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

        // POST: api/Configs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Config>> PostConfig(Config config)
        {
            _context.Config.Add(config);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfig", new { id = config.Id }, config);
        }

        // DELETE: api/Configs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig(int id)
        {
            var config = await _context.Config.FindAsync(id);
            if (config == null)
            {
                return NotFound();
            }

            _context.Config.Remove(config);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfigExists(int id)
        {
            return _context.Config.Any(e => e.Id == id);
        }
    }
}
