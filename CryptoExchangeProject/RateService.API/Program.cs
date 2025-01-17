using RateService.Application.Interfaces;
using RateService.Infrastructure.Caching;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Configurações de Swagger e Controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configuração do serviço HTTP para a Binance
builder.Services.AddHttpClient<IExchangeRateService, BinanceIntegrationService>(client =>
{
    client.BaseAddress = new Uri("https://api.binance.com");
});

// Obtenção da string de conexão do Redis
var redisConnectionString = builder.Configuration.GetSection("Redis")["ConnectionString"];

// ✅ Verificação da string de conexão ANTES de conectar
if (string.IsNullOrWhiteSpace(redisConnectionString))
{
    throw new ArgumentNullException(nameof(redisConnectionString), "Redis ConnectionString is missing in configuration.");
}

// Configuração do Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
builder.Services.AddScoped<ICacheService, RedisCacheService>();

var app = builder.Build();

// Configuração do pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();