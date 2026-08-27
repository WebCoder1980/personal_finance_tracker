using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Transactions;
using System.Xml.Linq;

namespace PersonalFinanceTracker.Transactions.Domain
{
    public class Category
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = null!;
        public CategoryType Type { get; private set; }
        public double? MonthlyAmount { get; private set; }
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();
        
        public static Category Create(Guid userId, string name, CategoryType type, double? monthlyAmount = null)
        {
            Category category = new();

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
    }
}
