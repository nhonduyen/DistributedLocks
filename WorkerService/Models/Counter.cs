using System.ComponentModel.DataAnnotations;

namespace WorkerService.Models
{
    public class Counter
    {
        public Guid Id { get; set; }
        public int Like { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        [MaxLength(50)]
        public string UpdatedBy { get; set; }
    }
}
