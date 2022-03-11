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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace cms_bd.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly DataContext dbContext;

        public UsersController(SignInManager<User> signInManager, UserManager<User> userManager, DataContext dbContext) {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers() {
            var users = await dbContext.Users.ToListAsync();
            return Ok(users.Select(user => new UserDTO(user)));
        }

        [HttpGet("users/logged")]
        public async Task<ActionResult<UserDTO>> GetLoggedUser() {
            var user = await userManager.GetUserAsync(User);
            if (user == null) {
                return Unauthorized();
            } else {
                return Ok(new UserDTO(user));
            }
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id) {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) {
                return NotFound();
            } else {
                return Ok(new UserDTO(user));
            }
        }

        [HttpPost("users/login")]
        public async Task<IActionResult> Login(Login model) {

            if (!(await UsernameTaken(model.UserName))) {
                ModelState.AddModelError("username", "User does not exist");
                return new ValidationFailedResult(ModelState, StatusCodes.Status400BadRequest);
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded) {
                return Ok();
            } else {
                ModelState.AddModelError("password", "Password is incorrect");
            }
            return new ValidationFailedResult(ModelState, StatusCodes.Status400BadRequest);
        }

        [HttpDelete("users/logout")]
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("users/register")]
        public async Task<IActionResult> Register(Register register) {
            User user = register.CreateUser();

            IdentityResult result = await userManager.CreateAsync(
                user,
                register.Password
            );
            if (!result.Succeeded) {
                foreach (IdentityError error in result.Errors) {
                    string field = error.Code.Contains("password", System.StringComparison.CurrentCultureIgnoreCase) ? "password" : "username";
                    ModelState.AddModelError(field, error.Description);
                }
                return new ValidationFailedResult(ModelState, StatusCodes.Status400BadRequest);
            }
            return Ok();
        }
        public class ValidationFailedResult : ObjectResult
        {
            public ValidationFailedResult(ModelStateDictionary modelState, int statusCode) : base(new ValidationResult(modelState, statusCode))
            {
                StatusCode = statusCode;
            }
        }

        private async Task<bool> UsernameTaken(string username) {
            return await userManager.FindByNameAsync(username) != null;
        }

    }
}
