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
        private readonly BooksDbContext _bookscontext;
        public SearchController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, BooksDbContext bookscontext)
        {
            _userManager = userManager;
            _bookscontext = bookscontext;
        }

          [Authorize(Roles = "Admin")]
        [HttpGet]
    public async Task<IActionResult> GetSearch(string user, string title)
        {
          if(title != null) {
            var book =  _bookscontext.Books.Where(q => q.Title.StartsWith(title));
            if(book != null) return Ok(book);
              else return NotFound();    
          }
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
