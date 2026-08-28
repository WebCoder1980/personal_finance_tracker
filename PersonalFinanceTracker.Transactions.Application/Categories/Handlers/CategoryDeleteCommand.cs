using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public record CategoryDeleteCommand(Guid Id, Guid UserId);
}
