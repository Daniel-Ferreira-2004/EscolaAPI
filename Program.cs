using EscolaAPI.Data;
using EscolaAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Pega chave da API Gemini
var apiKey = builder.Configuration["GeminiApiKey"];
if (string.IsNullOrEmpty(apiKey))
{
    throw new Exception("GeminiApiKey não encontrada no appsettings.json");
}

// Registro de serviços
builder.Services.AddHttpClient();

// Aqui está o ponto mais importante 👇
builder.Services.AddSingleton<GeminiServices>(provider => new GeminiServices(apiKey));

builder.Services.AddHttpClient<CepService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
