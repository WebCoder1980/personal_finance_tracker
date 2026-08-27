using Microsoft.AspNetCore.Mvc.Formatters;
using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PersonalFinanceTracker.Transactions.Domain
{
    public class Transaction
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid CategoryId { get; private set; }

        public DateOnly Date { get; private set; }

        public double Value { get; private set; }

        public string Comment { get; private set; } = string.Empty;

        public ICollection<Category> Category { get; private set; } = new List<Category>();

        private Transaction()
        {

        }

        public static Transaction Create(Guid categoryId, DateOnly date, double value, string comment = "")
        {
            Transaction transaction = new();

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
            if (newValue <= 0)
            {
                throw new DomainException("Value cannot be equal to or less than 0");
            }
            Value = newValue;
        }

        public void ChangeComment(string newValue) => Comment = newValue;
    }
}
