using AmazonWorkerService;
using AmazonWorkerService.Service;
using Microsoft.EntityFrameworkCore;
using warehouse_api.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);



builder.Services.AddHttpClient<IhttpRepository, httpRepository>();
builder.Services.AddScoped<IAmazonHelper, AmazonHelper>();

builder.Services.AddHostedService<Worker>();




builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("cloud-postgre"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});





var host = builder.Build();



host.Run();
