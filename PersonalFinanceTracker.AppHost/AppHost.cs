var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PersonalFinanceTracker_ApiGateway>("apigateway");

var userDatabase = builder.AddPostgres("user-postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("user-database");

builder.AddProject<Projects.PersonalFinanceTracker_Users_Infrastructure>("users")
    .WithReference(userDatabase)
    .WaitFor(userDatabase);

builder.Build()
    .Run();