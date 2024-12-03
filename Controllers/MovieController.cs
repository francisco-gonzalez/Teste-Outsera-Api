using Microsoft.AspNetCore.Mvc;
using OutseraApiTest.Models;
using OutseraApiTest.Services;

namespace OutseraApiTest.Controllers
{
    [ApiController]
    [Route("api/movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var movies = await _movieService.GetAllAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> Get(string title) {
            try
            {
                var movie = await _movieService.GetByTitleAsync(title);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie movie) {
            try
            {
                var movieCreated = await _movieService.AddAsync(movie);
                return CreatedAtAction(nameof(Get), new { title = movie.Title }, movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{title}")] 
        public async Task<IActionResult> Put(string title, [FromBody] Movie movie) {
            try
            {
                if (title != movie.Title) { 
                    return BadRequest(); 
                }
                await _movieService.UpdateAsync(movie);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> Delete(string title)
        {
            try
            {
                var movie = await _movieService.GetByTitleAsync(title);
                if (movie == null)
                {
                    return NotFound();
                }
                await _movieService.DeleteAsync(title);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
