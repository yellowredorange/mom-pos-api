global using ILogger = Serilog.ILogger;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using MomPosApi;
using MomPosApi.Models;
using MomPosApi.Repositories;
using MomPosApi.Data;
using MomPosApi.Services;
using AutoMapper;
using System.Text.Json.Serialization;
using System.Net;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
    });


// Configure Serilog
builder.Host.UseSerilog((context, configuration) => {
    configuration.MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File($"C://log/{builder.Configuration["Project"]}/log.txt", rollOnFileSizeLimit: true,
            rollingInterval: RollingInterval.Day)
        .ReadFrom.Configuration(context.Configuration);
});

// Configure CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost",
        builder => {
            builder.WithOrigins("http://localhost:5173/")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    options.AddPolicy("AllowAll",
        builder => {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
if (dbPassword != null) {
    var connectionString = builder.Configuration.GetConnectionString("MomPosContext");
    connectionString = connectionString.Replace("PLACEHOLDER_DB_PASSWORD", dbPassword);
    builder.Configuration["ConnectionStrings:MomPosContext"] = connectionString;
    Console.WriteLine("Updated connection string: " + connectionString);
} else { Console.WriteLine("nope"); }

builder.Services.AddDbContext<MomPosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MomPosContext")));

// Add repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IMenuConfigurationRepository, MenuConfigurationRepository>();
builder.Services.AddScoped<IMenuConfigurationService, MenuConfigurationService>();
builder.Services.AddScoped<IMenuItemOptionRepository, MenuItemOptionRepository>();
builder.Services.AddScoped<IMenuItemOptionService, MenuItemOptionService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(typeof(MomPosApi.Profiles.AutoMapperProfiles));

// Add controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
