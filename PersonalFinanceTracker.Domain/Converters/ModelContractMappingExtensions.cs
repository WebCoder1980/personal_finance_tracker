using PersonalFinanceTracker.Domain.Contracts;
using PersonalFinanceTracker.Domain.Models;

namespace PersonalFinanceTracker.Domain.Converters
{
    public static class ModelContractMappingExtensions
    {
        public static UserReference ToModel(this UserCreated userCreated)
        {
            return new UserReference
            {
                Id = userCreated.Id
            };
        }

        public static UserCreated ToModel(this User user)
        {
            return new UserCreated
            {
                Id = user.Id
            };
        }
    }
}
