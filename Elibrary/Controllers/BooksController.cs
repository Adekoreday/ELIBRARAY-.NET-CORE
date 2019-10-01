using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Elibrary.Models;

namespace Elibrary.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/books
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks()
        {
            using (var context = new BooksDbContext())
            {
            var books = await context.Books.ToListAsync();
             return Ok(books);
            }
        }

            // GET: api/books
        
        // [Authorize(Roles = "User")]
        // [HttpGet]
        // public ActionResult<string> Users()
        // {
        //     // using (var context = new BooksDbContext())
        //     // {
        //     // // var availabeBook = context.Books.Where(o => o.available == true);
        //     // }
        // }
        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBooks(int id)
        {
            using (var context = new BooksDbContext()) {
            var books = await context.Books.FindAsync(id);

            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
            }
        }
    
        // POST: api/books
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Books>> PostBooks([FromBody]Books books)
        {
            using (var context = new BooksDbContext()) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Books.Add(books);
            await context.SaveChangesAsync();
           return Ok(books);
        }
        }

                // DELETE api/book/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Books>> DeleteBooks(int id)
        {
                using (var context = new BooksDbContext()) {
                      var books = await context.Books.FindAsync(id);
            if (books==null)
            {
                return NotFound( new {
                    error = "book does not exist"
                });
            }

            else
            {
                context.Books.Remove(books);
                context.SaveChanges();
                return Ok( new {
                    message = "book deleted sucessfully"
                });
            }
                }
          
        }




    }
}
