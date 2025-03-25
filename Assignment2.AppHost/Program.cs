using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projects;


var builder = DistributedApplication.CreateBuilder(args);




// What about SQL Server?
var blogdb = builder.AddSqlServer("localhost")
    .AddDatabase("Assignment2Db"); 

var blazorServer = builder.AddProject<Projects.Assignment2Server>("blazorServer") //Blazor Server
    .WithReference(blogdb)
    .WaitFor(blogdb);

var apiService = builder.AddProject<Projects.Assignment2_ApiService>("apiservice") //Api Service
    .WithReference(blogdb)
    .WaitFor(blazorServer);


builder.AddProject<Projects.Assignment2_Web>("webfrontend") //Client Blazor
    .WithReference(apiService)
    .WaitFor(apiService);



builder.Build().Run();


