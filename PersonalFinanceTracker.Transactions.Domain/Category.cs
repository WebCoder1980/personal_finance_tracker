using PersonalFinanceTracker.ServiceDefaults.Exceptions;

namespace PersonalFinanceTracker.Transactions.Domain
{
    public class Category
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public CategoryType Type { get; private set; }
        public double? MonthlyAmount { get; private set; }
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        public static Category Create(Guid userId, string name, CategoryType type, double? monthlyAmount) => Create(Guid.NewGuid(), userId, name, type, monthlyAmount);
        public static Category Create(Guid id, Guid userId, string name, CategoryType type, double? monthlyAmount)
        {
            Category category = new();

            if (id == Guid.Empty)
            {
                throw new DomainException("Id cannot be empty");
            }
            category.Id = id;

            if (userId == Guid.Empty)
            {
                throw new DomainException("UserId cannot be empty");
            }
            category.UserId = userId;

            category.ChangeName(name);

            category.Type = type;

            category.ChangeMonthlyAmount(monthlyAmount);

            return category;
        }

        public void ChangeName(string newValue)
        {
            if (!(newValue.Length is >= 1 and <= 200))
            {
                throw new DomainException("Name must be between 1 and 200 chars long");
            }
            Name = newValue;
        }

        public void ChangeMonthlyAmount(double? newValue)
        {

            if (newValue is not null && newValue < 0)
            {
                throw new DomainException("MonthlyAmount cannot be less than 0");
            }

            MonthlyAmount = newValue;
        }

        public bool HasAccess(Guid userId)
        {
            if (UserId == userId)
            {
                return true;
            }
            return false;
        }
    }
}
