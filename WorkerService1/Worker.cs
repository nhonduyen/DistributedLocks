using WorkerService1.Services.Interfaces;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDistributedLockService _distributedLockService;

        public Worker(ILogger<Worker> logger, IDistributedLockService distributedLockService)
        {
            _logger = logger;
            _distributedLockService = distributedLockService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _distributedLockService.HandleUpdateLikesWithRetryAsync(stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}