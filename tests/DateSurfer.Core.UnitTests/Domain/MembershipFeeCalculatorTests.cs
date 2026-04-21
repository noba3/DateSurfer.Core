using FluentAssertions;
using Moq;
using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Domain.Enums;
using DateSurfer.Core.Domain.Interfaces;
using DateSurfer.Core.Application.Services;
using DateSurfer.Core.Application.Interfaces;

namespace DateSurfer.Core.UnitTests.Domain;

public class MembershipFeeCalculatorTests
{
    private readonly Mock<IFeeRuleRepository> _ruleRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly MembershipFeeCalculator _calculator;

    public MembershipFeeCalculatorTests()
    {
        _ruleRepositoryMock = new Mock<IFeeRuleRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _calculator = new MembershipFeeCalculator(_ruleRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task CalculateFeeAsync_UserInGermany_Premium_ShouldReturnCorrectFee()
    {
        // Arrange
        var birthDate = new DateTime(1990, 1, 1);
        var user = new User
        {
            Id = 1,
            Country = Country.Germany,
            DateOfBirth = birthDate
        };

        var today = DateTime.UtcNow;
        var expectedAge = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-expectedAge)) expectedAge--;

        var rule = new FeeRule
        {
            Country = Country.Germany,
            MembershipType = MembershipType.Premium,
            MinAge = 18,
            MaxAge = 99,
            MonthlyFee = 29.99m,
            IsActive = true
        };

        _userRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
        _ruleRepositoryMock.Setup(r => r.GetActiveRuleAsync(Country.Germany, MembershipType.Premium, expectedAge))
            .ReturnsAsync(rule);

        // Act
        var fee = await _calculator.CalculateFeeAsync(1, MembershipType.Premium);

        // Assert
        fee.Should().Be(29.99m);
    }

    [Fact]
    public async Task CalculateFeeAsync_UserNotFound_ShouldThrowException()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((User?)null);

        // Act
        Func<Task> act = async () => await _calculator.CalculateFeeAsync(999, MembershipType.Premium);

        // Assert
        await act.Should().ThrowAsync<DateSurfer.Core.Domain.Exceptions.FeeCalculationException>()
            .WithMessage("*not found*");
    }
}