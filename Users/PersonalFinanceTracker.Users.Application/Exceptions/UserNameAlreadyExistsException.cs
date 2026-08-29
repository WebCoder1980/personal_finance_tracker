using PersonalFinanceTracker.ServiceDefaults.Exceptions;

namespace PersonalFinanceTracker.Users.Application.Exceptions
{
    public class UserNameAlreadyExistsException : DomainException
    {
        public UserNameAlreadyExistsException() : base("UserName already exists")
        {

        }
    }
}
