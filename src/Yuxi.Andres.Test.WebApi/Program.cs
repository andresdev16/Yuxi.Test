﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SharedKernel.Api.ServiceCollectionExtensions;
using SharedKernel.Infrastructure.Caching;
using SharedKernel.Infrastructure.Cqrs.Commands;
using SharedKernel.Infrastructure.Cqrs.Queries;
using Yuxi.Andres.Test.Infrastructure;
using Yuxi.Andres.Test.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
string connectionString = builder.Configuration.GetConnectionString("TestConnectionSqlServer");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configure kernel services
builder.Services.AddInMemoryCommandBus();
builder.Services.AddInMemoryQueryBus();
builder.Services.AddInMemoryCache();
builder.Services.AddCore(builder.Configuration, connectionString);
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Yuxi.Andres.Test.WebApi", Version = "v1" }));
builder.Services.AddDbContext<TestContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TestContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<TestContext>>();
    try
    {
        context.Database.SetConnectionString(connectionString);
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error while migration was executing");
    }

}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseSharedKernelMetrics();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

