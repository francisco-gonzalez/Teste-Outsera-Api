using OutseraApiTest.Models;

namespace OutseraApiTest.Repositories
{
    public class MockMovieRepository : IMovieRepository
    {
        private List<Movie> movies = new List<Movie>
            {
                new Movie { Year = 1980, Title = "Movie1", Producers = "Producer1", Winner = "Yes" },
                new Movie { Year = 1982, Title = "Movie2", Producers = "Producer2", Winner = "Yes" },
                new Movie { Year = 1985, Title = "Movie3", Producers = "Producer1", Winner = "" },
                new Movie { Year = 1985, Title = "Movie4", Producers = "Producer1", Winner = "Yes" },
                new Movie { Year = 1987, Title = "Movie5", Producers = "Producer2", Winner = "Yes" },
                new Movie { Year = 1990, Title = "Movie6", Producers = "Producer3", Winner = "Yes" },
                new Movie { Year = 1990, Title = "Movie7", Producers = "Producer3", Winner = "" },
                new Movie { Year = 1995, Title = "Movie8", Producers = "Producer1", Winner = "Yes" }
            };

        public async Task<Movie> AddAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return movies;
            });
        }

        public Task<Movie> GetByMovieAsync(string movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
