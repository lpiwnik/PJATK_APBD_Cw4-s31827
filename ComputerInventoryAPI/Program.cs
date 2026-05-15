using ComputerInventoryAPI;
using ComputerInventoryAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPcService, PcService>();

var app = builder.Build();

// Automatyczne migracje przy starcie
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try 
    {
        var dbContext = services.GetRequiredService<AppDbContext>();
        
        // Ważne: W Dockerze SQL Server wstaje dłużej niż aplikacja.
        // MigrateAsync spróbuje połączyć się z bazą.
        await dbContext.Database.MigrateAsync();
        
        logger.LogInformation("Database updated and seeded successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Swagger dostępny w Development (w Docker Compose ustawiliśmy ASPNETCORE_ENVIRONMENT=Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint testowy po angielsku
app.MapGet("/test-db", async (AppDbContext db) =>
{
    bool canConnect = await db.Database.CanConnectAsync();
    return canConnect 
        ? Results.Ok(new { Message = "Connected to database successfully!" }) 
        : Results.Problem("Could not connect to the database.");
});

app.MapControllers();

app.Run();