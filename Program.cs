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
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
    });

builder.Services.AddHttpClient();

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
{
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173/")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<MomPosContext>(optionsAction: options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MomPosContext")));

// Add repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRepository<MenuItem>, MenuItemRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IMenuConfigurationRepository, MenuConfigurationRepository>();
builder.Services.AddScoped<IMenuConfigurationService, MenuConfigurationService>();
builder.Services.AddScoped<IRepository<MenuItemOption>, MenuItemOptionRepository>();
builder.Services.AddScoped<IMenuItemOptionService, MenuItemOptionService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(typeof(MomPosApi.Profiles.AutoMapperProfiles));


// Add controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

// //! 註冊 DataSeeder
// builder.Services.AddTransient<DataSeeder>();
// //! 註冊 ResetDatabase
// builder.Services.AddTransient<ResetDatabase>();

var app = builder.Build();

// //! 建立預設資料
// using (var scope = app.Services.CreateScope()) {
//     var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
//     await seeder.SeedDataAsync();
// }
// //! Reset 所有資料
// using (var scope = app.Services.CreateScope()) {
//     var reset = scope.ServiceProvider.GetRequiredService<ResetDatabase>();
//     await reset.Reset();
// }

// //! 做 Database Migration
// using (var scope = app.Services.CreateScope()) {
//     var services = scope.ServiceProvider;
//     try {
//         var context = services.GetRequiredService<MomPosContext>();
//         context.Database.Migrate();
//         Log.Information("Database migration completed successfully.");
//     } catch (Exception ex) {
//         Log.Error(ex, "An error occurred while migrating the database.");
//     }
// }


app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
