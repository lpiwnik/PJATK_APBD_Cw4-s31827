using ComputerInventoryAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Warto dodać Swaggera do testów
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- BLOK AUTOMATYCZNEJ MIGRACJI ---
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try 
    {
        await dbContext.Database.MigrateAsync();
        Console.WriteLine("Baza danych została zaktualizowana i zasilona danymi.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Błąd podczas migracji: {ex.Message}");
    }
}
// -----------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/test-db", async (AppDbContext db) =>
{
    bool canConnect = await db.Database.CanConnectAsync();
    return canConnect ? Results.Ok("Połączono z bazą!") : Results.Problem("Brak połączenia.");
});

app.MapControllers();

app.Run();