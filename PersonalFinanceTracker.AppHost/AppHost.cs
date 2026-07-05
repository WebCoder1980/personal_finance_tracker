var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ApiGateway>("apigateway");

var userDatabase = builder.AddPostgres("user-postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("user-database");

var transactionDatabase = builder.AddPostgres("transaction-postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("transaction-database");

var rabbitMq = builder.AddRabbitMQ("rabbitmq")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin();

builder.AddProject<Projects.Users>("users")
    .WithReference(userDatabase)
    .WithReference(rabbitMq);

builder.AddProject<Projects.Transactions>("transactions")
    .WithReference(transactionDatabase)
    .WithReference(rabbitMq);

builder.Build()
    .Run();