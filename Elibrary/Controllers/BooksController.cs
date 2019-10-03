using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Elibrary.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Elibrary.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksDbContext _context;
        private readonly IMapper _mapper;
        public BooksController(BooksDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/books
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
             return Ok(books);
        }

            // GET: api/books
        [Route("[action]")]
        [Authorize(Roles = "User")]
        [HttpGet]

        public async Task<ActionResult<Books>> Users()
        {
            var books = await _context.Books.ToListAsync();
            var availabeBook = books.Where(o => o.available == true);
            return Ok(availabeBook);
        }
        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> Get(int id)
        {
            var books = await _context.Books.FindAsync(id);

            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

    
        // POST: api/books
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Books>> Post([FromBody]Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Books.Add(books);
           var val =  await _context.SaveChangesAsync();

           return Ok(books);
        }


//
        //Patch books (borrow books)
       // : api/books/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<Books>> Borrow(int id, [FromBody] JsonPatchDocument<BooksDto> bookPatch)
        {
            var books = await _context.Books.FindAsync(id);

            if (books == null)
            {
                return NotFound();
            }
            var booksUpdate = _mapper.Map<BooksDto>(books);
            bookPatch.ApplyTo(booksUpdate, ModelState);
            _mapper.Map(booksUpdate, books);
             var isValid = TryValidateModel(books);
            if (!isValid) return BadRequest(ModelState);
            var updateStatus = await _context.SaveChangesAsync();
            return Ok(books);
        }


        // DELETE api/book/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Books>> Delete(int id)
        {
                      var books = await _context.Books.FindAsync(id);
            if (books==null)
            {
                return NotFound( new {
                    error = "book does not exist"
                });
            }
            else
            {
                _context.Books.Remove(books);
                _context.SaveChanges();
                return Ok( new {
                    message = "book deleted sucessfully"
                });
            }
          }
    }
}
