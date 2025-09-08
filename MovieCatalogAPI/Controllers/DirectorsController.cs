using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Data;
using MovieCatalogAPI.Models;

namespace MovieCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DirectorsController(AppDbContext context) => _context = context;

        // GET: api/directors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> GetDirectors() =>
            await _context.Directors.Include(d => d.Movies).ToListAsync();

        // GET: api/directors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> GetDirector(int id)
        {
            var director = await _context.Directors.Include(d => d.Movies)
                                                   .FirstOrDefaultAsync(d => d.Id == id);

            if (director == null) return NotFound();
            return director;
        }

        // GET: api/directors/5/movies
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByDirector(int id)
        {
            var director = await _context.Directors.Include(d => d.Movies)
                                                   .FirstOrDefaultAsync(d => d.Id == id);

            if (director == null) return NotFound();
            return director.Movies ?? new List<Movie>();
        }

        // POST: api/directors
        [HttpPost]
        public async Task<ActionResult<Director>> CreateDirector(Director director)
        {
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDirector), new { id = director.Id }, director);
        }

        // PUT: api/directors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirector(int id, Director director)
        {
            if (id != director.Id) return BadRequest();

            _context.Entry(director).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Directors.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/directors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null) return NotFound();

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
