using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DateSurfer.Core.Application.Services;
using DateSurfer.Core.Application.Interfaces;
using DateSurfer.Core.Domain.Interfaces;
using DateSurfer.Core.Domain.Enums;
using DateSurfer.Core.Infrastructure.Data;
using DateSurfer.Core.Infrastructure.Repositories;

namespace DateSurfer.Core.IntegrationTests;

public class SimpleIntegrationTests
{
    [Fact]
    public async Task CalculateFee_GermanUserPremium_Returns29_99()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new ApplicationDbContext(options);

        // Testdaten
        var user = new DateSurfer.Core.Domain.Entities.User
        {
            Username = "testuser",
            Email = "test@example.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            Country = Country.Germany,
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Users.Add(user);

        var feeRule = new DateSurfer.Core.Domain.Entities.FeeRule
        {
            Country = Country.Germany,
            MembershipType = MembershipType.Premium,
            MinAge = 18,
            MaxAge = 99,
            MonthlyFee = 29.99m,
            IsActive = true
        };
        dbContext.FeeRules.Add(feeRule);
        await dbContext.SaveChangesAsync();

        // Repositories und Service
        IUserRepository userRepository = new UserRepository(dbContext);
        IFeeRuleRepository feeRuleRepository = new FeeRuleRepository(dbContext);
        IMembershipFeeCalculator feeCalculator = new MembershipFeeCalculator(feeRuleRepository, userRepository);

        // Act
        var fee = await feeCalculator.CalculateFeeAsync(user.Id, MembershipType.Premium);

        // Assert
        fee.Should().Be(29.99m);
    }

    [Fact]
    public async Task CalculateFee_USUserPremium_Returns34_99()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new ApplicationDbContext(options);

        var user = new DateSurfer.Core.Domain.Entities.User
        {
            Username = "testuser",
            Email = "test@example.com",
            DateOfBirth = new DateTime(2000, 6, 15),
            Country = Country.USA,
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Users.Add(user);

        var feeRule = new DateSurfer.Core.Domain.Entities.FeeRule
        {
            Country = Country.USA,
            MembershipType = MembershipType.Premium,
            MinAge = 18,
            MaxAge = 99,
            MonthlyFee = 34.99m,
            IsActive = true
        };
        dbContext.FeeRules.Add(feeRule);
        await dbContext.SaveChangesAsync();

        IUserRepository userRepository = new UserRepository(dbContext);
        IFeeRuleRepository feeRuleRepository = new FeeRuleRepository(dbContext);
        IMembershipFeeCalculator feeCalculator = new MembershipFeeCalculator(feeRuleRepository, userRepository);

        // Act
        var fee = await feeCalculator.CalculateFeeAsync(user.Id, MembershipType.Premium);

        // Assert
        fee.Should().Be(34.99m);
    }

    [Fact]
    public async Task CalculateFee_InvalidUserId_ThrowsException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new ApplicationDbContext(options);

        IUserRepository userRepository = new UserRepository(dbContext);
        IFeeRuleRepository feeRuleRepository = new FeeRuleRepository(dbContext);
        IMembershipFeeCalculator feeCalculator = new MembershipFeeCalculator(feeRuleRepository, userRepository);

        // Act & Assert
        await Assert.ThrowsAsync<DateSurfer.Core.Domain.Exceptions.FeeCalculationException>(
            async () => await feeCalculator.CalculateFeeAsync(999, MembershipType.Premium));
    }
}