namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionCreateCommand(Guid Id, Guid UserId, Guid CategoryId, DateOnly Date, double Value, string? Comment)
    {
        public TransactionCreateCommand(Guid userId, Guid categoryId, DateOnly date, double value, string? comment) : this(Guid.NewGuid(), userId, categoryId, date, value, comment)
        {

        }
    }
}
