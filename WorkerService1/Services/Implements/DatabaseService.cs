using Microsoft.EntityFrameworkCore;
using WorkerService1.Data;
using WorkerService1.Models;
using WorkerService1.Services.Interfaces;

namespace WorkerService1.Services.Implements
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly IServiceProvider _serviceProvider;
        public DatabaseService(
            IServiceProvider serviceProvider, 
            ILogger<DatabaseService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<Counter> GetCounterAsync(CancellationToken cancellationToken = default)
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetService<CounterContext>();
            var counter = await _dbContext.Counter.OrderBy(x => x.Id).FirstOrDefaultAsync(cancellationToken);

            return counter;
        }

        public async Task HandleUpdateLikesAsync(CancellationToken cancellationToken = default)
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetService<CounterContext>();
            var counter = await GetCounterAsync(cancellationToken);

            if (counter == null)
            {
                return;
            }

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _logger.LogInformation($"Begin to update {counter.Id} like.");
                counter.Like = counter.Like + 1;
                counter.UpdatedBy = "Service 2";
                counter.LastUpdatedTime = DateTime.UtcNow;
                _dbContext.Counter.Update(counter);

                var counterHistory = new CounterHistory
                {
                    Id = Guid.NewGuid(),
                    CounterId = counter.Id,
                    Like = counter.Like,
                    UpdatedBy = counter.UpdatedBy,
                    LastUpdatedTime = counter.LastUpdatedTime,
                    RowVersion = counter.RowVersion
                };
                await _dbContext.AddAsync(counterHistory, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation($"Record {counter.Id} updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogInformation("Rolled back to saving point.");
                transaction.Rollback();
                throw;
            }

        }
    }
}
