var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ApiGateway>("apigateway");

var userDatabase = builder.AddPostgres("user-postgres")
    .AddDatabase("user-database");

var transactionDatabase = builder.AddPostgres("transaction-postgres")
    .AddDatabase("transaction-database");

builder.AddProject<Projects.Users>("users")
    .WithReference(userDatabase);

builder.AddProject<Projects.Transactions>("transactions")
    .WithReference(transactionDatabase);

builder.Build()
    .Run();