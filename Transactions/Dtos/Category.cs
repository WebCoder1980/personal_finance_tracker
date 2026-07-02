using System.ComponentModel.DataAnnotations;

namespace Transactions.Dtos
{
    public class CategoryUpsertRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public required string Name { get; set; }
        [Required]
        public required long TypeId { get; set; }
        [Range(0, double.MaxValue)]
        public required double MonthlyAmount { get; set; }
    }
}
