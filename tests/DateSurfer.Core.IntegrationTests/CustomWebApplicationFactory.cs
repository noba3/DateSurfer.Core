using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Domain.Enums;
using DateSurfer.Core.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DateSurfer.Core.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            // Add In-Memory database for testing
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("DateSurferTestDB_" + Guid.NewGuid().ToString()));

            // Build service provider and seed database
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();

            // Ensure database is created
            db.Database.EnsureCreated();

            // Seed test data
            SeedTestData(db);
        });
    }

    private static void SeedTestData(ApplicationDbContext db)
    {
        // Add test users
        db.Users.AddRange(
            new User
            {
                Id = 1,
                Username = "testuser1",
                Email = "test1@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Country = "Germany",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = 2,
                Username = "testuser2",
                Email = "test2@example.com",
                DateOfBirth = new DateTime(2000, 6, 15),
                Country = "USA",
                CreatedAt = DateTime.UtcNow
            }
        );

        // Add fee rules
        db.FeeRules.AddRange(
            new FeeRule
            {
                Id = 1,
                Country = "Germany",
                MembershipType = MembershipType.Premium,
                MinAge = 18,
                MaxAge = 99,
                MonthlyFee = 29.99m,
                IsActive = true
            },
            new FeeRule
            {
                Id = 2,
                Country = "USA",
                MembershipType = MembershipType.Premium,
                MinAge = 18,
                MaxAge = 99,
                MonthlyFee = 34.99m,
                IsActive = true
            },
            new FeeRule
            {
                Id = 3,
                Country = "Germany",
                MembershipType = MembershipType.Basic,
                MinAge = 18,
                MaxAge = 99,
                MonthlyFee = 9.99m,
                IsActive = true
            }
        );

        db.SaveChanges();
    }
}