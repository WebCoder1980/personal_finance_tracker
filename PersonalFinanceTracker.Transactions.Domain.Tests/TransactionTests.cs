using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace PersonalFinanceTracker.Transactions.Domain.Tests
{
    public class TransactionTests
    {
        [Fact]
        public void Test_Create_Ok()
        {
            // Arrange
            Guid categoryId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
            DateOnly date = new DateOnly(2026, 08, 27);
            const double value = 2000;
            const string comment = "Хлеб";

            // Act
            Transaction transaction = Transaction.Create(categoryId, date, value, comment);

            // Assert
            Assert.Equal(categoryId, transaction.CategoryId);
            Assert.Equal(date, transaction.Date);
            Assert.Equal(value, transaction.Value);
            Assert.Equal(comment, transaction.Comment);
        }

        [Fact]
        public void Test_Create_Ok_Without_comment()
        {
            // Arrange
            Guid categoryId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
            DateOnly date = new DateOnly(2026, 08, 27);
            const double value = 2000;

            // Act
            Transaction transaction = Transaction.Create(categoryId, date, value);

            // Assert
            Assert.Equal(categoryId, transaction.CategoryId);
            Assert.Equal(date, transaction.Date);
            Assert.Equal(value, transaction.Value);
            Assert.Equal("", transaction.Comment);
        }

        [Fact]
        public void Test_Create_CategoryId_empty()
        {
            // Arrange
            Guid categoryId = Guid.Empty;
            DateOnly date = new DateOnly(2026, 08, 27);
            const double value = 2000;
            const string comment = "Хлеб";

            // Act
            DomainException exception = Assert.Throws<DomainException>(() => Transaction.Create(categoryId, date, value, comment));

            // Assert
            Assert.Equal("CategoryId cannot be empty", exception.Message);
        }

        [Fact]
        public void Test_Create_Value_too_small()
        {
            // Arrange
            Guid categoryId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
            DateOnly date = new DateOnly(2026, 08, 27);
            const double value = 0;
            const string comment = "Хлеб";

            // Act
            DomainException exception = Assert.Throws<DomainException>(() => Transaction.Create(categoryId, date, value, comment));

            // Assert
            Assert.Equal("Value cannot be equal to or less than 0", exception.Message);
        }
    }
}
