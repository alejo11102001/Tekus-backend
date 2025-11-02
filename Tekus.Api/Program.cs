using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Data;
using Tekus.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conn));

// DI: repositorios (infrastructure) e interfaces (domain) + servicios de aplicacion
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<Tekus.Application.Services.IProviderAppService, Tekus.Application.Services.ProviderAppService>();

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await Tekus.Infrastructure.Data.DataSeeder.SeedAsync(db);
}

app.UseHttpsRedirection();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
