using PersonalFinanceTracker.ServiceDefaults.Exceptions;

namespace PersonalFinanceTracker.Transactions.Domain
{
    public class Transaction
    {
        public Guid Id { get; private set; }

        public Guid CategoryId { get; private set; }

        public DateOnly Date { get; private set; }

        public double Value { get; private set; }

        public string? Comment { get; private set; }

        public Category Category { get; private set; }

        private Transaction()
        {

        }

        public static Transaction Create(Guid categoryId, DateOnly date, double value, string? comment) => Create(Guid.NewGuid(), categoryId, date, value, comment);
        public static Transaction Create(Guid id, Guid categoryId, DateOnly date, double value, string? comment)
        {
            Transaction transaction = new();

            if (id == Guid.Empty)
            {
                throw new DomainException("Id cannot be empty");
            }
            transaction.Id = id;

            if (categoryId == Guid.Empty)
            {
                throw new DomainException("CategoryId cannot be empty");
            }
            transaction.CategoryId = categoryId;

            transaction.ChangeDate(date);

            transaction.ChangeValue(value);

            transaction.ChangeComment(comment);

            return transaction;
        }

        public void ChangeDate(DateOnly newValue) => Date = newValue;

        public void ChangeValue(double newValue)
        {
            if (newValue < 0)
            {
                throw new DomainException("Value cannot be less than 0");
            }
            Value = newValue;
        }

        public void ChangeComment(string? newValue) => Comment = newValue;

        public bool HasAccess(Guid userId)
        {
            if (Category.UserId == userId)
            {
                return true;
            }
            return false;
        }
    }
}
