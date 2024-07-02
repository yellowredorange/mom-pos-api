global using ILogger = Serilog.ILogger;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using MomPosApi;
using MomPosApi.Models;
using MomPosApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddControllers();
builder.Services.AddSingleton<IConnectionRepo, ConnectionRepo>();
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
