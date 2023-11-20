using APIarchitecturePractice.Contexts;
using APIarchitecturePractice.Controllers;
using APIarchitecturePractice.CustomMiddlewares;
using APIarchitecturePractice.Data_Access_Layer;
using APIarchitecturePractice.Extensions;
using APIarchitecturePractice.Helper;
using APIarchitecturePractice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddControllers();

// Code for dependency injection of context into the Repository
builder.Services.AddDbContext<DeliveryDBContext>(options =>
options.
UseSqlServer(builder.Configuration.GetConnectionString("homeConnString")));


// Registering for other dependencies in the DI container
builder.Services.AddScoped<IOrderRespository, OrderRespository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


// IOptions
builder.Services.Configure<Messages>(builder.Configuration.GetSection("Messages"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Use to enable xml documentation
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Architecture_Practice", Version = "V1" });
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        c.CustomSchemaIds(x => x.FullName);
    });


// In .NET 7, use below
//builder.Services.AddRateLimiter(options =>
//{
//    options.GlobalLimiter =
//    PartitionedRateLimiter.Create<HttpContext, string>(httpContext => RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(), factory: partition => new FixedWindowRateLimiterOptions
//    {
//        AutoReplenishment = true,
//        PermitLimit = 5,
//        QueueLimit = 0,
//        Window = TimeSpan.FromMinutes(1)
//    }));
//});



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


// Custom Middleware
app.ConfigureFirstCustomMiddleware();

//app.UseMiddleware<GlobalExceptionHandler>();

app.Run();
