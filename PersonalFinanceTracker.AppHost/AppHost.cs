var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ApiGateway>("apigateway");

var userDatabase = builder.AddPostgres("user-postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("user-database");

var transactionDatabase = builder.AddPostgres("transaction-postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("transaction-database");

builder.AddProject<Projects.Users>("users")
    .WithReference(userDatabase);

builder.AddProject<Projects.Transactions>("transactions")
    .WithReference(transactionDatabase);

builder.Build()
    .Run();