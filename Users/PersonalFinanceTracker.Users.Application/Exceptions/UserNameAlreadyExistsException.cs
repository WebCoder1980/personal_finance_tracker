using PersonalFinanceTracker.Users.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Exceptions
{
    public class UserNameAlreadyExistsException : DomainException
    {
        public UserNameAlreadyExistsException() : base("UserName already exists")
        {

        }
    }
}
