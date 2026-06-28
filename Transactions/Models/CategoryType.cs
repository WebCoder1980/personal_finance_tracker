using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Transactions.Models
{
    public class CategoryType
    {
        public long Id { get; set; }

        [Length(1, 200)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]    
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
