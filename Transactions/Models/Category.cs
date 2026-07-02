using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Transactions.Models
{
    public class Category
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; } = null!;
        public long TypeId { get; set; }
        public double MonthlyAmount { get; set; }

        [JsonIgnore]
        public CategoryType? Type { get; set; }

        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
