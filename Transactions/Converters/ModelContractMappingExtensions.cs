using Transactions.MessageBuses.Contracts;
using Transactions.Models;

namespace Transactions.Converters
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
    }
}
