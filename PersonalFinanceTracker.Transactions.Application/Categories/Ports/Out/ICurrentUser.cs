using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out
{
    public interface ICurrentUser
    {
        Guid Id { get; }
    }
}
