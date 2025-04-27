using Cayd.EntityFrameworkCore.Extensions.Test.Api.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#if NET8_0_OR_GREATER
var connectionString = builder.Configuration.GetConnectionString("Test8.0");
#else
var connectionString = builder.Configuration.GetConnectionString("Test6.0");
#endif

builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.Run();
