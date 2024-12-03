using OutseraApiTest.Models;
using OutseraApiTest.Repositories;

namespace OutseraApiTest.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            try
            {
                return await _movieRepository.AddAsync(movie);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao grava o Filme. Erro: {ex.Message}");
            }            
        }

        public async Task DeleteAsync(string title)
        {
            try
            {
                await _movieRepository.DeleteAsync(title);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir o Filme. Erro: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            try
            {
                return await _movieRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar Filmes. Erro: {ex.Message}");
            }
        }

        public async Task<Movie> GetByTitleAsync(string title)
        {
            try
            {
                return await _movieRepository.GetByTitleAsync(title);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o filmes {title}. Erro: {ex.Message}");
            }
        }

        public async Task<MinMaxProducerResponse> GetProducerMinMaxIntervalAsync(IEnumerable<Movie> movies)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var winners = movies.Where(x => x.IsWinner).ToList();
                    var producerIntervals = winners.SelectMany(m => m.Producers!.Replace(" and",",").Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(p => new { Producer = p.Trim(), m.Year }))
                                                .GroupBy(p => p.Producer)
                                                .Where(g => g.Count() > 1)
                                                .SelectMany(g =>
                                                {
                                                    var years = g.Select(x => x.Year).OrderBy(y => y).ToList();
                                                    return years.Zip(years.Skip(1), (prev, next) =>
                                                        new ProducerResponse(g.Key, next - prev, prev, next));
                                                })
                                                .ToList();
                    var maxInterval = producerIntervals.Max(r => r.Interval);
                    var minInterval = producerIntervals.Min(r => r.Interval);

                    var result = new MinMaxProducerResponse
                    {
                        Min = producerIntervals.Where(r => r.Interval == minInterval),
                        Max = producerIntervals.Where(r => r.Interval == maxInterval)
                    };
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao Obter o produtor com maior intervalo entre dois prêmios consecutivos, e o que obteve dois prêmios mais rápido. Erro: {ex.Message}.");
                }
            });
        }

        public async Task UpdateAsync(Movie movie)
        {
            try
            {
                await _movieRepository.UpdateAsync(movie);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar o Filme {movie.Title}. Erro: {ex.Message}");
            }
        }
    }
}
