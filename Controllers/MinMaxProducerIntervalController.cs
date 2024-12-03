using Microsoft.AspNetCore.Mvc;
using OutseraApiTest.Services;

namespace OutseraApiTest.Controllers
{
    [ApiController]
    [Route("api/min-max-producer-interval")]
    public class MinMaxProducerInterval : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MinMaxProducerInterval(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var movies = await _movieService.GetAllAsync();
                return Ok(await _movieService.GetProducerMinMaxIntervalAsync(movies));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
