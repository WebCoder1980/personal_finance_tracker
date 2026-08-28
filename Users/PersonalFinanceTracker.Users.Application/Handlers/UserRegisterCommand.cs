using PersonalFinanceTracker.ServiceDefaults.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Handlers
{
    public record UserRegisterCommand(Guid Id, string UserName, string Password, string Role)
    {
        public UserRegisterCommand(string userName, string password) : this(Guid.NewGuid(), userName, password, AppRoles.USER)
        {

        }
    }
}
