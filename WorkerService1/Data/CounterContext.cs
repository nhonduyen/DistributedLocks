using Microsoft.EntityFrameworkCore;
using WorkerService1.Models;

namespace WorkerService1.Data
{
    public class CounterContext : DbContext
    {
        public CounterContext(DbContextOptions<CounterContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Counter> Counter { get; set; }
        public DbSet<CounterHistory> CounterHistory { get; set; }
    }
}
