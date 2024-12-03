using CsvHelper;
using CsvHelper.Configuration;
using OutseraApiTest.Models;
using OutseraApiTest.Repositories;
using System.Globalization;
using System.Text;

namespace OutseraApiTest.Services
{
    public class CsvImportService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<CsvImportService> _logMovie;

        public CsvImportService(
            IMovieRepository movieRepository,
            ILogger<CsvImportService> logMovie)
        {
            _movieRepository = movieRepository;
            _logMovie = logMovie;
        }

        public async Task ImportFromCsvAsync(string filePath, string delimiter)
        {
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.UTF8,
                    Delimiter = delimiter, 
                    HeaderValidated = null, 
                    MissingFieldFound = null 
                };

                using var reader = new StreamReader(filePath, Encoding.UTF8);
                using var csv = new CsvReader(reader, configuration);

                csv.Context.RegisterClassMap<MovieMap>();

                var records = csv.GetRecords<Movie>().ToList();
                await _movieRepository.DeleteAllAsync();
                foreach (var record in records)
                {
                    await _movieRepository.AddAsync(record);
                }
                _logMovie.LogInformation($"Importação de CSV concluída. Total de registros: {records.Count}");
            }
            catch (Exception ex)
            {
                _logMovie.LogError($"Erro na importação do CSV: {ex.Message}");
                throw;
            }
        }
    }
}
