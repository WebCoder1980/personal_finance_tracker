using PersonalFinanceTracker.ServiceDefaults;
using PersonalFinanceTracker.Users.Application.Handlers;
using PersonalFinanceTracker.Users.Application.Ports.In;
using PersonalFinanceTracker.Users.Application.Ports.Out;
using PersonalFinanceTracker.Users.Infrastructure.Data;
using PersonalFinanceTracker.Users.Infrastructure.Dtos;
using PersonalFinanceTracker.Users.Infrastructure.Middleware;
using PersonalFinanceTracker.Users.Infrastructure.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddJwtBearerWithDefaults(builder.Configuration.GetSection("Jwt"), "Users");

builder.AddNpgsqlDbContext<AppDbContext>("user-database");

builder.Services.AddScoped<IUserRegisterHandler, UserRegisterHandler>();
builder.Services.AddScoped<IUserLoginHandler, UserLoginHandler>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

builder.Services.AddHostedService<DataInitializer>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

var app = builder.Build();
app.CreateDbContextCreated<AppDbContext>();

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
