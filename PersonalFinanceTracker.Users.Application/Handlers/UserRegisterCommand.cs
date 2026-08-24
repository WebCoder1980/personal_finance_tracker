using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Handlers
{
    public record UserRegisterCommand(string UserName, string Password);
}
