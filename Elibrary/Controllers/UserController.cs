using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

                // GET: api/user
    [Authorize(Roles = "Admin")]
        [HttpGet]
        public  ActionResult GetAllUsers()
        {
            var user = _userManager.Users;
            return Ok(user);
        }

  [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
public async Task<IActionResult> Delete(string id)
{
    var user = await _userManager.FindByIdAsync(id);
    if (user != null)
    {
        IdentityResult result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
           return Ok(new {
               message = "user deleted successfully"
           });
        else
         return StatusCode(500);
    }
     return BadRequest(new {
         error = "user does not exist"
     });
}
    }
}
