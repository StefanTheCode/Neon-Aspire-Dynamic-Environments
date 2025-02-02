var builder = DistributedApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;

var connectionString = builder.AddConnectionString(environment);

builder.AddProject<Projects.Blog_Api>("blog-api")
       .WithReference(connectionString);

builder.Build().Run();