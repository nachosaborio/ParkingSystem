using Microsoft.EntityFrameworkCore;
using Parking.Api;
using Parking.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Contexto>(options => options.UseSqlite(builder.Configuration.GetConnectionString("AppConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<Contexto>();
    context.Database.EnsureCreated();
    DatabaseSeeder.Seed(context);
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
