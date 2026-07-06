using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Users.Data;
using PersonalFinanceTracker.Users.MessageBuses;
using PersonalFinanceTracker.Users.Middleware;
using PersonalFinanceTracker.Users.Service;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddRabbitMQClient("rabbitmq");
builder.AddJwtBearerWithDefaults(builder.Configuration.GetSection("Jwt"), "Users");

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IMessageBus, RabbitMqMessageBus>();

builder.AddNpgsqlDbContext<AppDbContext>("user-database");

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));


var app = builder.Build();
app.EnsureDbContextCreated<AppDbContext>();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "API v1"));
}

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
