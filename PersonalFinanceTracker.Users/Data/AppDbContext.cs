using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Domain.Constants;
using PersonalFinanceTracker.Domain.Models;
using PersonalFinanceTracker.Users.Service;

namespace PersonalFinanceTracker.Users.Data
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
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.UserName).IsRequired().HasMaxLength(100);
                entity.HasIndex(x => x.UserName).IsUnique();
                entity.Property(x => x.PasswordHash).IsRequired();
                entity.Property(x => x.Role).IsRequired().HasMaxLength(50);

                entity.HasData(
                    new User() { Id = 1, UserName = "admin", PasswordHash = AuthUtil.HashPassword("admin_password"), Role = AppRoles.ADMIN },
                    new User() { Id = 2, UserName = "maxsmg", PasswordHash = AuthUtil.HashPassword("qweqwe"), Role = AppRoles.USER }
                );
            });
        }
    }
}
