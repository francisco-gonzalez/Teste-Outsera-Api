using LiteDB;
using OutseraApiTest.Models;
using OutseraApiTest.Services;

namespace OutseraApiTest.Repositories
{
    public class LiteDBMovieRepository : IMovieRepository
    {
        private readonly LiteDatabase _database;
        private readonly ILiteCollection<Movie> _movies;

        public LiteDBMovieRepository(string databasePath)
        {
            _database = new LiteDatabase(databasePath);
            _movies = _database.GetCollection<Movie>("Movie");
        }

        public Task<Movie> AddAsync(Movie movie)
        {            
            _movies.Insert(movie);
            return Task.FromResult(movie);
        }

        public async Task<Movie> GetByTitleAsync(string title)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _movies.FindOne(x => x.Title == title);
                }
                catch (Exception)
                {
                    throw;
                }
            });            
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _movies.FindAll().ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao buscar filmes. Erro: {ex.Message}.");
                }
            });
        }

        public async Task UpdateAsync(Movie movie)
        {
            await Task.Run(() =>
            {
                try
                {
                    return _movies.Update(movie);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao buscar filmes. Erro: {ex.Message}.");
                }
            });
        }

        public async Task DeleteAsync(string title)
        {
            await Task.Run(() =>
            {
                try
                {
                    return _movies.Delete(title);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao buscar filmes. Erro: {ex.Message}.");
                }
            });
        }

        public async Task DeleteAllAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    _movies.DeleteAll();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao excluir todos os filmes. Erro: {ex.Message}.");
                }
            });
        }
    }
}
