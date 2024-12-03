using OutseraApiTest.Models;
using OutseraApiTest.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace OutseraApiTest.Tests
{
    public class MovieServiceTests : IClassFixture<ServiceProviderFixture>
    {
        private readonly IMovieService _movieService;

        public MovieServiceTests(ServiceProviderFixture fixture)
        {
            _movieService = fixture.ServiceProvider.GetRequiredService<IMovieService>();
        }

        [Fact]
        public async Task GetProducerMinMaxInterval_TestAsync()
        {
            var movies = await _movieService.GetAllAsync();
            var result = await _movieService.GetProducerMinMaxIntervalAsync(movies);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Min);
            Assert.NotEmpty(result.Max);

            // Validando o Min
            Assert.Equal(2, result.Min.Count());
            Assert.Contains(result.Min, p => p.Producer == "Producer1" && p.Interval == 5);
            Assert.Contains(result.Min, p => p.Producer == "Producer2" && p.Interval == 5);

            // Validando Máximo
            Assert.Equal(1, result.Max.Count());
            Assert.Contains(result.Max, p => p.Producer == "Producer1" && p.Interval == 10);
        }
    }
}
