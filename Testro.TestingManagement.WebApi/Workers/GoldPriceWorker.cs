using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testro.TestingManagement.WebApi.Services;

namespace Testro.TestingManagement.WebApi.Workers
{
    public class GoldPriceWorker : IHostedService
    {
        private readonly IServiceProvider _services;

        public GoldPriceWorker(IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _services.CreateScope())
            {
                var goldService = scope.ServiceProvider
                    .GetRequiredService<NBPGoldService>();

                
                while (!cancellationToken.IsCancellationRequested)
                {
                    var prices = await goldService.GetGoldPriceAsync();
                    var cena = prices.FirstOrDefault()?.Cena;
                    Console.WriteLine(cena != null && cena > 200 ? "Sell" : "Buy");
                    // 5 min delay
                    await Task.Delay(300000, cancellationToken);
                } 
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}