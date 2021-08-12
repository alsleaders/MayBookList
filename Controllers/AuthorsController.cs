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
    // All of these routes will be at the base URL:     /api/Authors
    // That is what "api/[controller]" means below. It uses the name of the controller
    // in this case AuthorsController to determine the URL
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        // This is the variable you use to have access to your database
        private readonly DatabaseContext _context;

        // Constructor that recives a reference to your database context
        // and stores it in _context for you to use in your API methods
        public AuthorsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        //
        // Returns a list of all your Authors
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            // Uses the database context in `_context` to request all of the Authors, sort
            // them by row id and return them as a JSON array.
            return await _context.Authors.OrderBy(row => row.Id).ToListAsync();
        }

        // GET: api/Authors/5
        //
        // Fetches and returns a specific author by finding it by id. The id is specified in the
        // URL. In the sample URL above it is the `5`.  The "{id}" in the [HttpGet("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            // Find the author in the database using `FindAsync` to look it up by id
            var author = await _context.Authors.FindAsync(id);

            // If we didn't find anything, we receive a `null` in return
            if (author == null)
            {
                // Return a `404` response to the client indicating we could not find a author with this id
                return NotFound();
            }

            //  Return the author as a JSON object.
            return author;
        }

        // PUT: api/Authors/5
        //
        // Update an individual author with the requested id. The id is specified in the URL
        // In the sample URL above it is the `5`. The "{id} in the [HttpPut("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        // In addition the `body` of the request is parsed and then made available to us as a Author
        // variable named author. The controller matches the keys of the JSON object the client
        // supplies to the names of the attributes of our Author POCO class. This represents the
        // new values for the record.
        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            // If the ID in the URL does not match the ID in the supplied request body, return a bad request
            if (id != author.Id)
            {
                return BadRequest();
            }

            // Tell the database to consider everything in author to be _updated_ values. When
            // the save happens the database will _replace_ the values in the database with the ones from author
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                // Try to save these changes.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Ooops, looks like there was an error, so check to see if the record we were
                // updating no longer exists.
                if (!AuthorExists(id))
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
            return Ok(author);
        }

        // POST: api/Authors
        //
        // Creates a new author in the database.
        //
        // The `body` of the request is parsed and then made available to us as a Author
        // variable named author. The controller matches the keys of the JSON object the client
        // supplies to the names of the attributes of our Author POCO class. This represents the
        // new values for the record.
        //
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            // Indicate to the database context we want to add this new record
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            // Return a response that indicates the object was created (status code `201`) and some additional
            // headers with details of the newly created object.
            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        //
        // Deletes an individual author with the requested id. The id is specified in the URL
        // In the sample URL above it is the `5`. The "{id} in the [HttpDelete("{id}")] is what tells dotnet
        // to grab the id from the URL. It is then made available to us as the `id` argument to the method.
        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            // Find this author by looking for the specific id
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                // There wasn't a author with that id so return a `404` not found
                return NotFound();
            }

            // Tell the database we want to remove this record
            _context.Authors.Remove(author);

            // Tell the database to perform the deletion
            await _context.SaveChangesAsync();

            // Return a copy of the deleted data
            return Ok(author);
        }

        // Private helper method that looks up an existing author by the supplied id
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(author => author.Id == id);
        }
    }
}
