using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Users.Domain.Models;

namespace PersonalFinanceTracker.Users.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.UserName).IsRequired().HasMaxLength(100);
                entity.HasIndex(x => x.UserName).IsUnique();
                entity.Property(x => x.PasswordHash).IsRequired();
                entity.Property(x => x.Role).IsRequired().HasMaxLength(50);
            });
        }
    }
}
