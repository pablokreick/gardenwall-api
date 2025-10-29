using Microsoft.EntityFrameworkCore;
using OverTheGardenWallAPI;
using OverTheGardenWallAPI.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OverTheGardenWallDbContext>(options =>
    options.UseSqlite("Data Source=OverTheGardenWall.db"));


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

app.UseHttpsRedirection();

app.MapGet("/character", async(
    OverTheGardenWallDbContext db,
    string? nombre,
    string? especie,
    int page = 1,
    int pageSize = 10) =>
{
    var query = db.Characters.AsQueryable();

    if (!string.IsNullOrWhiteSpace(nombre))
    {
        query = query.Where(c => c.Nombre.Contains(nombre));
    }
    if (!string.IsNullOrWhiteSpace(especie))
    {
        query = query.Where(c => c.Especie == especie);
    }

    var total = query.CountAsync();
    var results = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return Results.Ok(new { Total = total, Page = page, PageSize = pageSize, Results = results });
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OverTheGardenWallDbContext>();
    SeedData.Initialize(db);
    db.Database.Migrate();
}


app.Run();