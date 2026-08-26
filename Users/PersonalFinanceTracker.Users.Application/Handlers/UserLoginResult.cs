using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Handlers
{
    public record UserLoginResult(string Token, string UserName, string Role);
}
