using Transactions.Data;
using Transactions.Service;
using Transactions.Service.Auth;
using Users.MessageBuses;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRabbitMQClient("rabbitmq");

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.AddJwtBearerWithDefaults(builder.Configuration.GetSection("Jwt"), "Transactions");
builder.AddNpgsqlDbContext<AppDbContext>("transaction-database");

builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddHostedService<RabbitMqMessageBus>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.EnsureDbContextCreated<AppDbContext>();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "API v1"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
