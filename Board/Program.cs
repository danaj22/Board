using Board.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<BoardContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BoardDbConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<BoardContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}
var users = dbContext.Users.ToList();
if (!users.Any())
{
    var user1 = new User 
    { 
        FullName = "User One", 
        Email = "user1@test.com" , 
        Address = new Address 
        { 
            City = "Warszawa", 
            Street = "Szeroka" 
        } 
    };

    var user2 = new User
    {
        FullName = "User Two",
        Email = "user2@test.com",
        Address = new Address
        {
            City = "Kraków",
            Street = "Długa"
        }
    };

    dbContext.Users.AddRange(user1, user2);

    dbContext.SaveChanges();
}

var tags = dbContext.Tags.ToList();
if (!tags.Any())
{
    var tag1 = new Tag { Value = "Web" };
    var tag2 = new Tag { Value = "UI" };
    var tag3 = new Tag { Value = "Desktop" };
    var tag4 = new Tag { Value = "API" };
    var tag5 = new Tag { Value = "Service" };

    dbContext.AddRange(tag1, tag2, tag3, tag4, tag5);

    dbContext.SaveChanges();
}

app.MapGet("/data", async (BoardContext db) => 
{
    var user = await db.Users
                        .Include(x => x.Address)
                        .Include(x => x.Comments)
                        .FirstAsync(x => x.Id == Guid.Parse("5cb27c3f-32d9-4474-cbc2-08da10ab0e61"));


    return new { user };
});

app.MapPost("/update", async (BoardContext db) =>
{
    var epicToEdit = await db.Epics.FirstAsync(x => x.Id == 1);
    
    epicToEdit.StateId = 1;

    await db.SaveChangesAsync();

    return epicToEdit;
});

app.MapPost("/add", async (BoardContext db) =>
{
    var address = new Address { City = "Wrocław", Country = "Polska", Street = "Krótka" };
    var user = new User { FullName = "Jan Kowalski", Email = "jan.kowalski@gmail.com", Address = address };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return   user;
});

app.MapDelete("/delete", async (BoardContext db) =>
{
    var user = await db.Users
                        .Include(x => x.Comments)
                        .FirstAsync(x => x.Id == Guid.Parse("C25E6A2C-6B92-423C-CC25-08DA10AB0E61"));

    db.Remove(user);
    await db.SaveChangesAsync();
});
       
app.Run();