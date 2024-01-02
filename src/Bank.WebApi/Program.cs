using Application.Interfaces;
using Application.Services;
using DbUp;
using Domain.Models.Dtos.Responses;
using Infrastructure.Repositories;
using Npgsql;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

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
