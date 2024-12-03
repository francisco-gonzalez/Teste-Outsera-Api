using OutseraApiTest.Models;

namespace OutseraApiTest.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> AddAsync(Movie movie);
        Task<Movie> GetByTitleAsync(string title);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(string title);
        Task DeleteAllAsync();        
    }
}
