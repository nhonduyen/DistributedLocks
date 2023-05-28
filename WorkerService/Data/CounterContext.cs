using Microsoft.EntityFrameworkCore;
using WorkerService.Models;

namespace WorkerService.Data
{
    public class CounterContext : DbContext
    {
        public CounterContext(DbContextOptions<CounterContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;
            modelBuilder.Entity<Counter>().Property(e => e.RowVersion).IsRowVersion();
            modelBuilder.Entity<Counter>().HasData(
                new Counter
                {
                    Id = Guid.NewGuid(),
                    Like = 0,
                    CreatedAt = now,
                    LastUpdatedTime = now,
                    UpdatedBy = string.Empty
                }
                );
        }

        public DbSet<Counter> Counter { get; set; }
        public DbSet<CounterHistory> CounterHistory { get; set; }
    }
}
