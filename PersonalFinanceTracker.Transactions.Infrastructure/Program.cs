using PersonalFinanceTracker.ServiceDefaults;
using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Infrastructure.Data;
using PersonalFinanceTracker.Transactions.Infrastructure.Util;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.AddJwtBearerWithDefaults(builder.Configuration.GetSection("Jwt"), "Transactions");
builder.AddNpgsqlDbContext<AppDbContext>("transaction-database");

builder.Services.AddScoped<ICategoryGetHandler, CategoryGetHandler>();
builder.Services.AddScoped<ICategoryCreateHandler, CategoryCreateHandler>();
builder.Services.AddScoped<ICategoryUpdateHandler, CategoryUpdateHandler>();
builder.Services.AddScoped<ICategoryDeleteHandler, CategoryDeleteHandler>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.CreateDbContextCreated<AppDbContext>();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
