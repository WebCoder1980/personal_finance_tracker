using PersonalFinanceTracker.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Users.Application.Ports.Out
{
    public interface IUserRepository
    {
        Task<bool> UserNameIsBusyAsync(string userName, CancellationToken token);

        Task SaveAsync(User user, CancellationToken token);
        Task SaveChangesAsync();
    }
}
