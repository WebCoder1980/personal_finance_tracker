namespace PersonalFinanceTracker.Domain.Contracts
{
    public class MessageBusEvent<T>
    {
        public required Guid Id { get; set; }
        public required DateTime OccuredAt { get; set; }
        public required T Payload { get; set; }
    }
}
