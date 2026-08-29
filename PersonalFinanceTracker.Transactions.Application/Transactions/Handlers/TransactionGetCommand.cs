using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionGetCommand(Guid UserId);
}
