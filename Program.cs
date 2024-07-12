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

// Register DbContext with SQL Server
builder.Services.AddDbContext<MomPosApi.Data.MomPosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MomPosContext")));


// Add services to the container
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
builder.Services.AddAutoMapper(typeof(MomPosApi.Profiles.AutoMapperProfiles)); // 明確指定配置類型

// Add controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure middleware
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
