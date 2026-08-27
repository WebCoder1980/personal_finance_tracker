using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PersonalFinanceTracker.Transactions.Domain
{
    public enum CategoryType
    {
        Income,
        Expence
    }
}
