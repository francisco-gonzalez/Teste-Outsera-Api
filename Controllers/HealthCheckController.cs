using Microsoft.AspNetCore.Mvc;

namespace OutseraApiTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private IConfiguration Configuration;

        public HealthCheckController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public ObjectResult Index()
        {
            try
            {
                return Ok("API no ar!");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
