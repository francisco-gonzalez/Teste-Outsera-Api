using OutseraApiTest.Models;
using OutseraApiTest.Repositories;

namespace OutseraApiTest.Services
{
    public interface IMovieService
    {
        Task<Movie> AddAsync(Movie movie);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByTitleAsync(string title);
        Task<MinMaxProducerResponse> GetProducerMinMaxIntervalAsync(IEnumerable<Movie> movies);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(string title);
    }
}
