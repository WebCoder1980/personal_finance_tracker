using Users.MessageBuses.Contracts;
using Users.Models;

namespace Users.Converters
{
    public static class ModelContractMappingExtensions
    {
        public static UserCreated ToModel(this User user)
        {
            return new UserCreated
            {
                Id = user.Id
            };
        }
    }
}
