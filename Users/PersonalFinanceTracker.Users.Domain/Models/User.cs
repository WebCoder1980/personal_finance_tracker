using PersonalFinanceTracker.ServiceDefaults.Constants;
using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Users.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string UserName { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Role { get; private set; } = AppRoles.USER;

        private User()
        {
        }

        public static User Register(string userName, string passwordHash, string role)
        {
            User user = new User();

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

            if (!AppRoles.IsValid(role))
            {
                throw new DomainException("Role is invalid");
            }
            user.Role = role;

            return user;
        }
    }
}
