using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Type).IsRequired();
                entity.Property(x => x.MonthlyAmount).IsRequired(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CategoryId).IsRequired();
                entity.Property(x => x.Date).IsRequired();
                entity.Property(x => x.Value).IsRequired();
                entity.Property(x => x.Comment).IsRequired(false);
            });
        }
    }
}
