using Transactions.Dtos;
using Transactions.Models;

namespace Transactions.Converters
{
    public static class ModelDtoMappingExtensions
    {
        public static Category ToModel(this CategoryUpsertRequest categoryCreateRequest)
        {
            return new Category()
            {
                Name = categoryCreateRequest.Name,
                TypeId = categoryCreateRequest.TypeId,
                MonthlyAmount = categoryCreateRequest.MonthlyAmount,
            };
        }

        public static CategoryUpsertRequest ToDto(this Category category)
        {
            return new CategoryUpsertRequest()
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
    }
}
