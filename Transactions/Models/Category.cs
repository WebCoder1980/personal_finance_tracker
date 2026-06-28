using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Transactions.Models
{
    public class Category
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }

        [Length(1, 200)]
        public string Name { get; set; } = null!;

        public long TypeId { get; set; }

        [Range(0, double.MaxValue)]
        public double MonthlyAmount { get; set; }

        [JsonIgnore]
        public CategoryType Type { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
