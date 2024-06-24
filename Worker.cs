using AmazonWorkerService.Service;
using BackgroundServiceAmazon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using warehouse_api.Data;
using warehouse_api.Model.Domain;

namespace AmazonWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        //private readonly DataContext _context;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, /*DataContext context,*/ IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            //_context = context;     
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetRequiredService<IhttpRepository>();
                        var context = scope.ServiceProvider.GetRequiredService<IAmazonHelper>();
                        _logger.LogInformation("Fetching Amazon orders at: {time}", DateTimeOffset.Now);

                        try
                        {


                            var orders = await repository.GetAmazonAsync();
                            _logger.LogInformation("Fetched {count} orders", orders.Count);
                            _logger.LogInformation("Saving orders to the database...");
                            await context.saveAsync();
                            _logger.LogInformation("Orders saved successfully.");

                            _logger.LogInformation("Saving order items to the database...");
                            await context.saveOrderItem();
                            _logger.LogInformation("Order items saved successfully.");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "An error occurred while processing orders.");
                        }

                    }

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                   

                }
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

    }
}
