using PersonalFinanceTracker.Users.Application.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Ports.In
{
    public interface IUserRegisterHandler
    {
        Task<UserRegisterResult> ExecuteAsync(UserRegisterCommand command, CancellationToken token);
    }
}
