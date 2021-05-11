using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MayBookList.Models;

namespace MayBookList.Controllers
{
    // All of these routes will be at the base URL:     /api/Books
    // That is what "api/[controller]" means below. It uses the name of the controller
    // in this case BooksController to determine the URL
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // This is the variable you use to have access to your database
        private readonly DatabaseContext _context;

        // Constructor that recives a reference to your database context
        // and stores it in _context for you to use in your API methods
        public BooksController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Books
        //
        // Returns a list of all your Books
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            // Uses the database context in `_context` to request all of the Books, sort
            // them by row id and return them as a JSON array.
            return await _context.Books.OrderBy(row => row.Id).ToListAsync();
        }

        // GET: api/Books/5
        //
        // Fetches and returns a specific book by finding it by id. The id is specified in the
        // URL. In the sample URL above it is the `5`.  The "{id}" in the [HttpGet("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            // Find the book in the database using `FindAsync` to look it up by id
            var book = await _context.Books.FindAsync(id);

            // If we didn't find anything, we receive a `null` in return
            if (book == null)
            {
                // Return a `404` response to the client indicating we could not find a book with this id
                return NotFound();
            }

            //  Return the book as a JSON object.
            return book;
        }

        // PUT: api/Books/5
        //
        // Update an individual book with the requested id. The id is specified in the URL
        // In the sample URL above it is the `5`. The "{id} in the [HttpPut("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        // In addition the `body` of the request is parsed and then made available to us as a Book
        // variable named book. The controller matches the keys of the JSON object the client
        // supplies to the names of the attributes of our Book POCO class. This represents the
        // new values for the record.
        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            // If the ID in the URL does not match the ID in the supplied request body, return a bad request
            if (id != book.Id)
            {
                return BadRequest();
            }

            // Tell the database to consider everything in book to be _updated_ values. When
            // the save happens the database will _replace_ the values in the database with the ones from book
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                // Try to save these changes.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Ooops, looks like there was an error, so check to see if the record we were
                // updating no longer exists.
                if (!BookExists(id))
                {
                    // If the record we tried to update was already deleted by someone else,
                    // return a `404` not found
                    return NotFound();
                }
                else
                {
                    // Otherwise throw the error back, which will cause the request to fail
                    // and generate an error to the client.
                    throw;
                }
            }

            // Return a copy of the updated data
            return Ok(book);
        }

        // POST: api/Books
        //
        // Creates a new book in the database.
        //
        // The `body` of the request is parsed and then made available to us as a Book
        // variable named book. The controller matches the keys of the JSON object the client
        // supplies to the names of the attributes of our Book POCO class. This represents the
        // new values for the record.
        //
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            // Indicate to the database context we want to add this new record
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Return a response that indicates the object was created (status code `201`) and some additional
            // headers with details of the newly created object.
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        //
        // Deletes an individual book with the requested id. The id is specified in the URL
        // In the sample URL above it is the `5`. The "{id} in the [HttpDelete("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            // Find this book by looking for the specific id
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                // There wasn't a book with that id so return a `404` not found
                return NotFound();
            }

            // Tell the database we want to remove this record
            _context.Books.Remove(book);

            // Tell the database to perform the deletion
            await _context.SaveChangesAsync();

            // Return a copy of the deleted data
            return Ok(book);
        }

        // Private helper method that looks up an existing book by the supplied id
        private bool BookExists(int id)
        {
            return _context.Books.Any(book => book.Id == id);
        }
    }
}
