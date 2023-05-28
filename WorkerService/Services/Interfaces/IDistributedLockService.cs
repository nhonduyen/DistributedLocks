﻿namespace WorkerService.Services.Interfaces
{
    public interface IDistributedLockService
    {
        public Task HandleUpdateLikesAsync(CancellationToken cancellationToken = default);
        public Task HandleUpdateLikesWithRetryAsync(CancellationToken cancellationToken = default);
    }
}
