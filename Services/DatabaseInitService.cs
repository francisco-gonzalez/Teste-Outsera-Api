using OutseraApiTest.Models;

namespace OutseraApiTest.Services
{
    public class DatabaseInitService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<DatabaseInitService> _log;
        public DatabaseInitService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<DatabaseInitService> log)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _log = log;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Executa apenas uma vez na inicialização
            await InitializeDatabaseAsync(stoppingToken);
        }

        private async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var csvImportService = scope.ServiceProvider.GetRequiredService<CsvImportService>();
                    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                    var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                    string csvPath = configuration["ImportSettings:CsvFilePath"];
                    string delimiter = configuration["ImportSettings:Delimiter"];

                    if (string.IsNullOrEmpty(csvPath))
                    {
                        csvPath = Path.Combine(environment.ContentRootPath, "Data", "movielist.csv");
                    }

                    if (File.Exists(csvPath))
                    {
                        await csvImportService.ImportFromCsvAsync(csvPath, delimiter);
                    }
                    else
                    {
                        _log.LogWarning($"Arquivo CSV não encontrado: {csvPath}");
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError($"Erro na inicialização do banco de dados: {ex.Message}");
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("Serviço de inicialização do banco de dados está parando.");
            return base.StopAsync(cancellationToken);
        }
    }
}
