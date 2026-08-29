using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.ServiceDefaults.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message = "Not found") : base(message)
        {

        }
    }
}
