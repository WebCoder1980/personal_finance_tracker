using PersonalFinanceTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalFinanceTracker.Domain.Dtos
{
    public class TransactionUpsertRequest
    {
        [Required]
        public required long CategoryId { get; set; }
        [Required]
        public required DateOnly Date { get; set; }
        [Required]
        [Range(0, long.MaxValue)]
        public required double Value { get; set; }
        [Required]
        [StringLength(1000)]
        public required string? Comment { get; set; }
    }
}
