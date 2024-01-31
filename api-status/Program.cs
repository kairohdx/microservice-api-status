var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços necessários.
builder.Services.AddControllers();
builder.Services.AddScoped<ISocketService, SocketService>();
builder.Services.AddScoped<SocketService>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<IApiStatusHub, ApiStatusHub>();
builder.Services.AddScoped<ApiStatusHub>();
builder.Services.AddScoped<SocketUpdateService>();

builder.Services.AddSingleton<MongoDbContext>((provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new MongoDbContext(configuration);
}));

var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
