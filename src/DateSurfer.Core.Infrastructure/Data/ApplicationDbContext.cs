using Microsoft.EntityFrameworkCore;
using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<FeeRule> FeeRules => Set<FeeRule>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            entity.Property(u => u.Country).HasMaxLength(50);
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasOne(m => m.User)
                .WithOne(u => u.Membership)
                .HasForeignKey<Membership>(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(m => m.Type).HasConversion<int>();
        });

        modelBuilder.Entity<FeeRule>(entity =>
        {
            entity.Property(r => r.Country).HasMaxLength(50);
            entity.Property(r => r.MembershipType).HasConversion<int>();
            entity.Property(r => r.MonthlyFee).HasPrecision(18, 2);
        });

        // Seed Fee Rules (Country als string)
        modelBuilder.Entity<FeeRule>().HasData(
            new FeeRule { Id = 1, Country = "Germany", MembershipType = MembershipType.Basic, MinAge = 18, MaxAge = 99, MonthlyFee = 9.99m, IsActive = true },
            new FeeRule { Id = 2, Country = "Germany", MembershipType = MembershipType.Premium, MinAge = 18, MaxAge = 99, MonthlyFee = 29.99m, IsActive = true },
            new FeeRule { Id = 3, Country = "Germany", MembershipType = MembershipType.VIP, MinAge = 18, MaxAge = 99, MonthlyFee = 59.99m, IsActive = true },
            new FeeRule { Id = 4, Country = "USA", MembershipType = MembershipType.Basic, MinAge = 18, MaxAge = 99, MonthlyFee = 12.99m, IsActive = true },
            new FeeRule { Id = 5, Country = "USA", MembershipType = MembershipType.Premium, MinAge = 18, MaxAge = 99, MonthlyFee = 34.99m, IsActive = true }
        );
    }
}