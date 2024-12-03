using OutseraApiTest.Factories;
using OutseraApiTest.Repositories;
using OutseraApiTest.Services;

namespace OutseraApiTest.Tests
{
    public class ServiceProviderFixture
    {
        public ServiceProvider ServiceProvider { get; }

        public ServiceProviderFixture()
        {
            var services = new ServiceCollection();
            services.AddTransient<IMovieRepository>(provider =>new MockMovieRepository());
            services.AddTransient<IMovieService, MovieService>();
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
