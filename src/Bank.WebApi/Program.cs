using Application;
using Application.Interfaces;
using Application.Services;
using DbUp;
using Domain.Models.Dtos.Responses;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HopShop", Version = "v1" });

    // Include the XML comments file
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddApplicationsToDIContainer();
builder.Services.AddInfrastructureToDIContainer();

string? dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(dbConnectionString));

EnsureDatabase.For.PostgresqlDatabase(dbConnectionString);
var upgrader = DeployChanges.To
.PostgresqlDatabase(dbConnectionString)
.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
.LogToConsole()
.Build();

upgrader.PerformUpgrade();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
