using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using DVHome.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<UserContext> (opt => opt
    .UseNpgsql(
        "Host=" + Environment.GetEnvironmentVariable("DB_HOST") +";" + 
        "Port="+ Environment.GetEnvironmentVariable("DB_PORT") + ";" + 
        "Database="+ Environment.GetEnvironmentVariable("DB_NAME") + ";" +
        "Username="+ Environment.GetEnvironmentVariable("DB_USER") + ";" +
        "Password="+ Environment.GetEnvironmentVariable("DB_PASS")
        ));


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

app.UseAuthorization();

app.MapControllers();

app.Run();
