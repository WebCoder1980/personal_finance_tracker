namespace PersonalFinanceTracker.ServiceDefaults.Exceptions
{
    public class PermissionDeniedException : DomainException
    {
        public PermissionDeniedException(string message = "Permission denied") : base(message)
        {
        }
    }
}
