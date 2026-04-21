using Microsoft.EntityFrameworkCore;
using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<FeeRule> FeeRules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            entity.Property(u => u.Country).HasConversion<int>();
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
            entity.Property(r => r.Country).HasConversion<int>();
            entity.Property(r => r.MembershipType).HasConversion<int>();
            entity.Property(r => r.MonthlyFee).HasPrecision(18, 2);  // 18 Stellen insgesamt, davon 2 Nachkommastellen
        });

        // Seed Fee Rules
        modelBuilder.Entity<FeeRule>().HasData(
            new FeeRule { Id = 1, Country = Country.Germany, MembershipType = MembershipType.Basic, MinAge = 18, MaxAge = 99, MonthlyFee = 9.99m, IsActive = true },
            new FeeRule { Id = 2, Country = Country.Germany, MembershipType = MembershipType.Premium, MinAge = 18, MaxAge = 99, MonthlyFee = 29.99m, IsActive = true },
            new FeeRule { Id = 3, Country = Country.Germany, MembershipType = MembershipType.VIP, MinAge = 18, MaxAge = 99, MonthlyFee = 59.99m, IsActive = true },
        // USA Fee Rules
            new FeeRule { Id = 4, Country = Country.USA, MembershipType = MembershipType.Basic, MinAge = 18, MaxAge = 99, MonthlyFee = 12.99m, IsActive = true },
            new FeeRule { Id = 5, Country = Country.USA, MembershipType = MembershipType.Premium, MinAge = 18, MaxAge = 99, MonthlyFee = 34.99m, IsActive = true },
            new FeeRule { Id = 6, Country = Country.USA, MembershipType = MembershipType.VIP, MinAge = 18, MaxAge = 99, MonthlyFee = 69.99m, IsActive = true }
        );
    }
}