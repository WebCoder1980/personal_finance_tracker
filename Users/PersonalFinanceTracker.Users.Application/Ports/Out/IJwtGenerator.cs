using PersonalFinanceTracker.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Ports.Out
{
    public interface IJwtGenerator
    {
        string Execute(User user);
    }
}
