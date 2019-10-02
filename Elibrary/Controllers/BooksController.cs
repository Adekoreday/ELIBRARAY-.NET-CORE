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
        [Route("[action]")]
        [Authorize(Roles = "User")]
        [HttpGet]

        public async Task<ActionResult<Books>> Users()
        {
            using (var context = new BooksDbContext())
            {
                 var books = await context.Books.ToListAsync();
            var availabeBook = books.Where(o => o.available == true);
            return Ok(availabeBook);
            }
        }
        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> Get(int id)
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

        //PUT books (borrow books)
       // : api/books/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<Books>> Borrow(int id, [FromBody] bool bookStatus)
        {
            using (var context = new BooksDbContext()) {
            var books = await context.Books.FindAsync(id);

            if (books == null)
            {
                return NotFound();
            }
            books.available = bookStatus;
            await context.SaveChangesAsync();
            return Ok(books);
            }
        }
    
        // POST: api/books
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Books>> Post([FromBody]Books books)
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
        public async Task<ActionResult<Books>> Delete(int id)
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
