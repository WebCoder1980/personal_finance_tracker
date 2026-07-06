using Newtonsoft.Json.Linq;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Models;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonalFinanceTracker.Domain.Converters
{
    public static class ModelDtoMappingExtensions
    {
        public static Category ToModel(this CategoryUpsertRequest categoryUpsertRequest)
        {
            return new Category
            {
                Name = categoryUpsertRequest.Name,
                TypeId = categoryUpsertRequest.TypeId,
                MonthlyAmount = categoryUpsertRequest.MonthlyAmount,
            };
        }

        public static CategoryUpsertRequest ToDto(this Category category)
        {
            return new CategoryUpsertRequest
            {
                Name = category.Name,
                TypeId = category.TypeId,
                MonthlyAmount = category.MonthlyAmount,
            };
        }
        public static void UpdateFrom(this Category model, CategoryUpsertRequest dto)
        {
            model.Name = dto.Name;
            model.TypeId = dto.TypeId;
            model.MonthlyAmount = dto.MonthlyAmount;
        }

        public static Transaction ToModel(this TransactionUpsertRequest transactionUpsertRequest)
        {
            return new Transaction
            {
                CategoryId = transactionUpsertRequest.CategoryId,
                Date = transactionUpsertRequest.Date,
                Value = transactionUpsertRequest.Value,
                Comment = transactionUpsertRequest.Comment,
            };
        }
        public static TransactionUpsertRequest ToDto(this Transaction transaction)
        {
            return new TransactionUpsertRequest
            {
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
                Value = transaction.Value,
                Comment = transaction.Comment
            };
        }
        public static void UpdateFrom(this Transaction model, TransactionUpsertRequest dto)
        {
            model.CategoryId = dto.CategoryId;
            model.Date = dto.Date;
            model.Value = dto.Value;
            model.Comment = dto.Comment;
        }
    }
}
