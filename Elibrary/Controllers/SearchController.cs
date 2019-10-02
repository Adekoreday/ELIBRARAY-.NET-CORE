using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Elibrary.Models;


namespace Elibrary.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SearchController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
        }

          [Authorize(Roles = "Admin")]
        [HttpGet]
    public async Task<IActionResult> GetSearch(string user, string title)
        {

          if(user != null) {
            var users = await _userManager.FindByNameAsync(user);
            if(users != null) return Ok(users);
              else return NotFound();    
          }
          return BadRequest(new {
            error = "add a valid search param"
          });
        }

    }
}
