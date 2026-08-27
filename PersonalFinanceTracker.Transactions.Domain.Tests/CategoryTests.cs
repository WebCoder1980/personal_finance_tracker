using PersonalFinanceTracker.ServiceDefaults.Exceptions;

namespace PersonalFinanceTracker.Transactions.Domain.Tests
{
    public class CategoryTests
    {
        [Fact]
        public void Test_Create_Ok()
        {
            // Arrange
            Guid userId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
            const string name = "Подработка на выходных";
            const CategoryType type = CategoryType.Income;
            const double monthlyAmount = 10000;

            // Act
            Category category = Category.Create(userId, name, type, monthlyAmount);

            // Assert
            Assert.Equal(userId, category.UserId);
            Assert.Equal(name, category.Name);
            Assert.Equal(type, category.Type);
            Assert.Equal(monthlyAmount, category.MonthlyAmount);
        }

        [Fact]
        public void Test_Create_Ok_Without_monthlyAmount()
        {
            // Arrange
            Guid userId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
            const string name = "Подработка на выходных";
            const CategoryType type = CategoryType.Income;

            // Act
            Category category = Category.Create(userId, name, type);

            // Assert
            Assert.Equal(userId, category.UserId);
            Assert.Equal(name, category.Name);
            Assert.Equal(type, category.Type);
            Assert.Null(category.MonthlyAmount);
        }

        [Fact]
        public void Test_Create_UserId_empty()
        {
            // Arrange
            Guid userId = Guid.Empty;
            const string name = "Подработка на выходных";
            const CategoryType type = CategoryType.Income;
            const double monthlyAmount = 10000;

            // Act
            DomainException exception = Assert.Throws<DomainException>(() => Category.Create(userId, name, type, monthlyAmount));

            // Assert
            Assert.Equal("UserId cannot be empty", exception.Message);
        }

        [Fact]
        public void Test_Create_Name_too_short()
        {
            // Arrange
            Guid userId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
            const string name = "";
            const CategoryType type = CategoryType.Income;
            const double monthlyAmount = 10000;

            // Act
            DomainException exception = Assert.Throws<DomainException>(() => Category.Create(userId, name, type, monthlyAmount));

            // Assert
            Assert.Equal("Name must be between 1 and 200 chars long", exception.Message);
        }

        [Fact]
        public void Test_Create_Name_too_long()
        {
            // Arrange
            Guid userId = Guid.Parse("12345678-1234-1234-1234-123456789abc");

            string name = "";
            for (int i = 0; i < 201; i++)
            {
                name += "W";
            }

            const CategoryType type = CategoryType.Income;
            const double monthlyAmount = 10000;

            // Act
            DomainException exception = Assert.Throws<DomainException>(() => Category.Create(userId, name, type, monthlyAmount));

            // Assert
            Assert.Equal("Name must be between 1 and 200 chars long", exception.Message);
        }

        [Fact]
        public void Test_Create_MonthlyAmount_negative()
        {
            // Arrange
            Guid userId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
            const string name = "Подработка на выходных";
            const CategoryType type = CategoryType.Income;
            const double monthlyAmount = -10000;

            // Act
            DomainException exception = Assert.Throws<DomainException>(() => Category.Create(userId, name, type, monthlyAmount));

            // Assert
            Assert.Equal("MonthlyAmount cannot be less than 0", exception.Message);
        }
    }
}
