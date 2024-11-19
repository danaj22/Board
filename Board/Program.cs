using Board.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapGet("/tags", async (BoardContext db) => 
{
    var epics = await db.Epics.Where(x => x.StateId == 4).OrderBy(x => x.Priority).ToListAsync();
    //refactor this...
    var user = await db.Comments.GroupBy(x => x.AuthorId).Select(g => new { key = g.Key, count = g.Count() }).OrderByDescending(x => x.count).Take(1).FirstAsync();
    var users = await db.Users.FirstAsync(x => x.Id == user.key);
    return new { epics, users };
});
       
app.Run();