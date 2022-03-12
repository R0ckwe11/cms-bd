#nullable disable
using Microsoft.AspNetCore.Mvc;
using cms_bd.Data;
using cms_bd.DTOs;

namespace cms_bd.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/posts/5
        [HttpGet("posts/{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(new PostDTO(post));
        }
    }
}
