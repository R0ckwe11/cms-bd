using cms_bd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cms_bd.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AdminController(SignInManager<User> signInManager, UserManager<User> userManager) {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [Route("")]
        [Route("~/")]
        public IActionResult Admin()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AdminLogin(string username, string password) {

            if (!(await UsernameTaken(username))) {
                ModelState.AddModelError("username", "User does not exist");
                return new UsersController.ValidationFailedResult(ModelState, StatusCodes.Status400BadRequest);
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(username, password, true, false);
            if (result.Succeeded)
            {
                return Redirect("/coreadmin");
            } else {
                ModelState.AddModelError("password", "Password is incorrect");
            }
            return new UsersController.ValidationFailedResult(ModelState, StatusCodes.Status400BadRequest);
        }

        private async Task<bool> UsernameTaken(string username) {
            return await userManager.FindByNameAsync(username) != null;
        }
    }
}
