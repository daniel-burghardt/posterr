using Microsoft.EntityFrameworkCore;
using Posterr.Models;
using Posterr.Repositories;
using Posterr.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
builder.Services.AddSqlServer<PosterrDbContext>(connectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IPostsRepository, PostsRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<PostsService>();
builder.Services.AddTransient<UsersService>();

var app = builder.Build();

// Apply any pending migrations
using var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<PosterrDbContext>();
await db.Database.MigrateAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();