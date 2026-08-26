using PersonalFinanceTracker.Users.Application.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Ports.In
{
    public interface IUserLoginHandler
    {
        Task<UserLoginResult> ExecuteAsync(UserLoginCommand command, CancellationToken token);
    }
}
