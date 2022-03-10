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
    [Route("api/")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private readonly DataContext _context;

        public ConfigsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/main
        [HttpGet("main")]
        public async Task<ActionResult<MainPageDTO>> GetMainPage()
        {
            var backgroundImage = await _context.Config
                .FirstOrDefaultAsync(t => t.Key == "BackgroundImage");

            var backgroundColor = await _context.Config
                .FirstOrDefaultAsync(t => t.Key == "BackgroundColor");

            var activePosts = await _context.Posts
                .Where(t => t.IsVisible == 1)
                .OrderBy(t => t.Order)
                .ToListAsync();

            // List<Image> activePostsImages;
            // foreach (var ap in activePosts)
            // {
            //     activePosts.Add(_context.Images.Include(task => task.FileName));
            // }

            var menuPosts = await _context.Posts
                .Where(t => t.IsVisible == 1 && t.IsInMenu == 1)
                .OrderBy(t => t.Order)
                .ToListAsync();

            return Ok(new MainPageDTO(backgroundImage, backgroundColor, activePosts, menuPosts));
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
            if (id != config.ID)
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

            return CreatedAtAction("GetConfig", new { id = config.ID }, config);
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
            return _context.Config.Any(e => e.ID == id);
        }
    }
}
