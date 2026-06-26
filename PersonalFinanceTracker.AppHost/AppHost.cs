var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ApiGateway>("apigateway");

builder.AddProject<Projects.Transactions>("transactions");

builder.Build().Run();
