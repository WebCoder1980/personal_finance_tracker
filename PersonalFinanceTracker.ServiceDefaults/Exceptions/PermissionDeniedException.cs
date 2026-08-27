using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.ServiceDefaults.Exceptions
{
    public class PermissionDeniedException : DomainException
    {
        public PermissionDeniedException(string message = "Permission denied") : base(message)
        {
        }
    }
}
