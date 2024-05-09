using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TodoApi.Models;
using TodoApi.Services;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{environment}.json", true, true);

builder.Services.Configure<TaskStoreDatabaseSettings>(builder.Configuration.GetSection("TaskStoreDatabase"));
builder.Services.AddSingleton<TasksService>();

// Add services to the container.

builder.Services.AddControllers();
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
