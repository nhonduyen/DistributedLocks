using WorkerService.Models;

namespace WorkerService.Services.Interfaces
{
    public interface IDatabaseService
    {
        public Task HandleUpdateLikesAsync(CancellationToken cancellationToken = default);
        public Task<Counter> GetCounterAsync(CancellationToken cancellationToken = default);
    }
}
