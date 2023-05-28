using WorkerService1.Models;

namespace WorkerService1.Services.Interfaces
{
    public interface IDatabaseService
    {
        public Task HandleUpdateLikesAsync(CancellationToken cancellationToken = default);
        public Task<Counter> GetCounterAsync(CancellationToken cancellationToken = default);
    }
}
