using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RedLockNet;
using WorkerService1.Services.Interfaces;
using WorkerService1.Settings;

namespace WorkerService1.Services.Implements
{
    public class DistributedLockService : IDistributedLockService
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<DistributedLockService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDistributedLockFactory _distributedLockFactory;

        public DistributedLockService(
            IDatabaseService databaseService,
            ILogger<DistributedLockService> logger,
            IConfiguration configuration,
            IDistributedLockFactory distributedLockFactory)
        {
            _databaseService = databaseService;
            _logger = logger;
            _configuration = configuration;
            _distributedLockFactory = distributedLockFactory;
        }

        public async Task HandleUpdateLikesAsync(CancellationToken cancellationToken = default)
        {
            var counter = await _databaseService.GetCounterAsync(cancellationToken);
            if (counter == null)
            {
                return;
            }

            var redisSetting = _configuration.GetSection("Redis").Get<RedisSetting>();
            var resource = $"lock: {counter.Id}";
            var expiry = TimeSpan.FromSeconds(redisSetting.Expriry);

            await using (var redlock = await _distributedLockFactory.CreateLockAsync(resource, expiry))
            {
                // make sure we got the lock
                if (redlock.IsAcquired)
                {
                    _logger.LogInformation($"Lock: {resource} available. Updating like...");
                    await _databaseService.HandleUpdateLikesAsync(cancellationToken);
                    await Task.Delay(1000); // hold the lock
                }
                else
                {
                    _logger.LogInformation($"Lock: {resource} not available.");
                }
            }
            // the lock is automatically released at the end of the using block
        }

        public async Task HandleUpdateLikesWithRetryAsync(CancellationToken cancellationToken = default)
        {
            var counter = await _databaseService.GetCounterAsync(cancellationToken);
            if (counter == null)
            {
                return;
            }

            var redisSetting = _configuration.GetSection("Redis").Get<RedisSetting>();
            var resource = $"lock: {counter.Id}";
            var expiry = TimeSpan.FromSeconds(redisSetting.Expriry);
            var wait = TimeSpan.FromSeconds(redisSetting.Wait);
            var retry = TimeSpan.FromSeconds(redisSetting.Retry);

            await using (var redlock = await _distributedLockFactory.CreateLockAsync(resource, expiry, wait, retry))
            {
                // make sure we got the lock
                if (redlock.IsAcquired)
                {
                    _logger.LogInformation($"Lock: {resource} available. Updating like...");
                    await _databaseService.HandleUpdateLikesAsync(cancellationToken);
                    await Task.Delay(1000); // hold the lock
                }
                else
                {
                    _logger.LogInformation($"Lock: {resource} not available.");
                }
            }
            // the lock is automatically released at the end of the using block
        }
    }
}
