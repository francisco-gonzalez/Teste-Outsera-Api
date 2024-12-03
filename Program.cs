using Microsoft.OpenApi.Models;
using OutseraApiTest.Factories;
using OutseraApiTest.Repositories;
using OutseraApiTest.Services;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var databaseType = configuration.GetValue("DatabaseType", DatabaseFactory.DatabaseType.LiteDB);
var apiKey = configuration["ApiKey"];
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Outsera Api Test", Version = "1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Name = "x-api-key",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = $"Authorization by x-api-key inside request's header - {apiKey}",
        Scheme = "ApiKeyScheme"
    });

    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    var requirement = new OpenApiSecurityRequirement { { key, new List<string>() } };
    c.AddSecurityRequirement(requirement);
});

builder.Services.AddSingleton<IMovieRepository>(
        DatabaseFactory.CreateMovieRepository(databaseType, connectionString)
    );
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddTransient<CsvImportService>();
builder.Services.AddHostedService<DatabaseInitService>();

var app = builder.Build();

// Configure o pipeline de solicitação HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api/HealthCheck", StringComparison.OrdinalIgnoreCase))
    {
        await next(); // Permite acesso anônimo
        return;
    }

    if (!context.Request.Headers.ContainsKey("x-api-key"))
    {
        context.Response.StatusCode = 400; // Bad Request
        await context.Response.WriteAsync("Não foi encontrado 'x-api-key' no header da requisição.");
        return;
    }

    var apiKey = context.Request.Headers["x-api-key"].ToString();

    // Obtenha a instância de IConfiguration diretamente do construtor
    var configuration = builder.Configuration;

    // Busque o valor da chave da API
    var expectedApiKey = configuration["ApiKey"];

    if (string.IsNullOrWhiteSpace(apiKey) || apiKey != expectedApiKey)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Invalid 'x-api-key' header.");
        return;
    }

    await next();
});

app.MapControllers();

app.Run();