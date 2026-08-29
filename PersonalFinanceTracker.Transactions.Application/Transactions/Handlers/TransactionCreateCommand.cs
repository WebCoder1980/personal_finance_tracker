namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionCreateCommand(Guid Id, Guid CategoryId, DateOnly Date, double Value, string? Comment)
    {
        public TransactionCreateCommand(Guid categoryId, DateOnly date, double value, string? comment) : this(Guid.NewGuid(), categoryId, date, value, comment)
        {

        }
    }
}
