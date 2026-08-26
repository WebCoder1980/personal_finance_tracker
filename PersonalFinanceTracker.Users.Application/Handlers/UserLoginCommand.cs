using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Handlers
{
    public record UserLoginCommand(string UserName, string Password);
}
