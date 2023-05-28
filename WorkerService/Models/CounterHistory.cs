using System.ComponentModel.DataAnnotations;

namespace WorkerService.Models
{
    public class CounterHistory
    {
        public Guid Id { get; set; }
        public Guid CounterId { get; set; }
        public int Like { get; set; }
        [MaxLength(18)]
        public byte[] RowVersion { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        [MaxLength(50)]
        public string UpdatedBy { get; set; }
    }
}
