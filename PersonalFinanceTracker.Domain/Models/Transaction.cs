using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Domain.Models
{
    public class Transaction
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long CategoryId { get; set; }

        public DateOnly Date { get; set; }

        [Range(0, long.MaxValue)]
        public double Value { get; set; }

        public string? Comment { get; set; }

        [JsonIgnore]
        public Category Category { get; set; } = null!;
    }
}
