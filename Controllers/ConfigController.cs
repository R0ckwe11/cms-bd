﻿#nullable disable
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
    public class ConfigController : ControllerBase
    {
        private readonly DataContext _context;

        public ConfigController(DataContext context)
        {
            _context = context;
        }

        // GET: api/main
        [HttpGet("main-page")]
        public async Task<ActionResult<MainPageDTO>> GetMainPage()
        {
            var backgroundImage = await _context.Config
                .FirstOrDefaultAsync(t => t.Key == "BackgroundImage");

            var backgroundColor = await _context.Config
                .FirstOrDefaultAsync(t => t.Key == "BackgroundColor");

            var contentTitle = await _context.Config
                .FirstOrDefaultAsync(t => t.Key == "ContentTitle");

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

            return Ok(new MainPageDTO(backgroundImage, backgroundColor, contentTitle, activePosts, menuPosts));
        }
    }
}
