using PersonalFinanceTracker.ServiceDefaults.Constants;
using PersonalFinanceTracker.ServiceDefaults.Exceptions;

namespace PersonalFinanceTracker.Users.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }

        private User()
        {

        }

        public static User Register(string userName, string passwordHash) => Register(Guid.NewGuid(), userName, passwordHash, AppRoles.USER);
        public static User Register(Guid id, string userName, string passwordHash, string role)
        {
            User user = new User();

            user.Id = id;

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new DomainException("UserName cannot be empty");
            }
            if (!(userName.Length is >= 5 and <= 50))
            {
                throw new DomainException("UserName must be between 5 and 50 chars long");
            }
            user.UserName = userName;

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new DomainException("PasswordHash cannot be empty");
            }
            user.PasswordHash = passwordHash;

            user.Role = role;

            return user;
        }
    }
}
