using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Domain.Models;

namespace PersonalFinanceTracker.Transactions.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserReference> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryType>(entity =>
            {
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
                entity.HasData(
                    new CategoryType() { Id = 1, Name = "Доход" },
                    new CategoryType() { Id = 2, Name = "Расход" }
                );
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(x => x.UserId).IsRequired();
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(x => x.TypeId).IsRequired();
                entity.Property(x => x.MonthlyAmount).IsRequired();
                entity.HasData(
                    new Category() { Id = 1, UserId = 1, Name = "Основная работа программистом", TypeId = 1, MonthlyAmount = 70000 },
                    new Category() { Id = 2, UserId = 1, Name = "Подработка на выходных", TypeId = 1, MonthlyAmount = 25000 },
                    new Category() { Id = 3, UserId = 1, Name = "Коммунальные платяжи", TypeId = 2, MonthlyAmount = 5000 },
                    new Category() { Id = 4, UserId = 1, Name = "Еда", TypeId = 2, MonthlyAmount = 15000 },
                    new Category() { Id = 5, UserId = 1, Name = "Онлайн подписки", TypeId = 2, MonthlyAmount = 5000 },
                    new Category() { Id = 6, UserId = 1, Name = "Одежда и обувь", TypeId = 2, MonthlyAmount = 5000 },
                    new Category() { Id = 7, UserId = 1, Name = "Расходники", TypeId = 2, MonthlyAmount = 5000 }
                );
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(x => x.UserId).IsRequired();
                entity.Property(x => x.CategoryId).IsRequired();
                entity.Property(x => x.Date).IsRequired();
                entity.Property(x => x.Value).IsRequired();
                entity.HasData(
                    new Transaction() { Id = 1, UserId = 1, CategoryId = 1, Date = new DateOnly(2026, 7, 1), Value = 70000, Comment = "Зарплата за июнь" },
                    new Transaction() { Id = 2, UserId = 1, CategoryId = 2, Date = new DateOnly(2026, 7, 2), Value = 25000, Comment = "Подработка на выходных" },
                    new Transaction() { Id = 3, UserId = 1, CategoryId = 4, Date = new DateOnly(2026, 7, 3), Value = 15000, Comment = "Покупки продуктов" },
                    new Transaction() { Id = 4, UserId = 1, CategoryId = 4, Date = new DateOnly(2026, 7, 5), Value = 3500, Comment = "Доставка еды" },
                    new Transaction() { Id = 5, UserId = 1, CategoryId = 3, Date = new DateOnly(2026, 7, 5), Value = 5000, Comment = "Коммунальные платежи" },
                    new Transaction() { Id = 6, UserId = 1, CategoryId = 5, Date = new DateOnly(2026, 7, 10), Value = 500, Comment = "Netflix подписка" },
                    new Transaction() { Id = 7, UserId = 1, CategoryId = 5, Date = new DateOnly(2026, 7, 10), Value = 300, Comment = "Spotify подписка" },
                    new Transaction() { Id = 8, UserId = 1, CategoryId = 6, Date = new DateOnly(2026, 7, 12), Value = 8000, Comment = "Новая куртка" },
                    new Transaction() { Id = 9, UserId = 1, CategoryId = 7, Date = new DateOnly(2026, 7, 15), Value = 2000, Comment = "Бытовая химия" },
                    new Transaction() { Id = 10, UserId = 1, CategoryId = 7, Date = new DateOnly(2026, 7, 15), Value = 12000, Comment = "Новая сковорода" },
                    new Transaction() { Id = 11, UserId = 1, CategoryId = 7, Date = new DateOnly(2026, 7, 20), Value = 4500, Comment = "Медицинские препараты" },
                    new Transaction() { Id = 12, UserId = 1, CategoryId = 4, Date = new DateOnly(2026, 7, 22), Value = 1200, Comment = "Кофе в кафе" },
                    new Transaction() { Id = 13, UserId = 1, CategoryId = 1, Date = new DateOnly(2026, 7, 25), Value = 5000, Comment = "Бонус за проект" },
                    new Transaction() { Id = 14, UserId = 1, CategoryId = 7, Date = new DateOnly(2026, 7, 28), Value = 600, Comment = "Такси домой" },
                    new Transaction() { Id = 15, UserId = 1, CategoryId = 2, Date = new DateOnly(2026, 7, 30), Value = 15000, Comment = "Дополнительная смена на подработке" }
                );
            });
        }
    }
}
