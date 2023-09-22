using Microsoft.AspNetCore.Mvc;
using TestExchangeRates.Database;
using TestExchangeRates.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddHttpClient();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition =
        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

RegisterDIServices(builder.Services);

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandling>();
HandleDatabaseServices(app);

app.MapGet("exchangeRates/table/{tableType}", async (string tableType, [FromServices] ExchangeRatesManager exchangeRatesManager) =>
{
    var response = await exchangeRatesManager.GetLatestRates(tableType);
    return Results.Json(response);
});

app.Run();

static void RegisterDIServices(IServiceCollection services)
{
    services.AddScoped<ErrorHandling>();
    services.AddScoped<ExchangeRatesManager>();
}

static void HandleDatabaseServices(WebApplication app)
{
    var scope = app.Services.CreateScope();

    var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbcontext.Database.EnsureCreated();
}
