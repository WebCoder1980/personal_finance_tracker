var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ApiGateway>("apigateway");

builder.AddProject<Projects.Transactions>("transactions");

var database = builder.AddPostgres("user-postgres")
    .AddDatabase("user-database");
builder.AddProject<Projects.Users>("users")
    .WithReference(database);

builder.Build()
    .Run();