using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Testro.TestingManagement.WebApi.Workers
{
    public class TimeWorker : BackgroundService
    {
        private readonly ILogger<TimeWorker> _logger;

        public TimeWorker(ILogger<TimeWorker> logger)
        {
            _logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}