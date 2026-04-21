using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Domain.Enums;
using DateSurfer.Core.Domain.Exceptions;
using DateSurfer.Core.Domain.Interfaces;
using DateSurfer.Core.Application.Interfaces;

namespace DateSurfer.Core.Application.Services;

public class MembershipFeeCalculator : IMembershipFeeCalculator
{
    private readonly IFeeRuleRepository _feeRuleRepository;
    private readonly IUserRepository _userRepository;

    public MembershipFeeCalculator(
        IFeeRuleRepository feeRuleRepository,
        IUserRepository userRepository)
    {
        _feeRuleRepository = feeRuleRepository;
        _userRepository = userRepository;
    }

    public async Task<decimal> CalculateFeeAsync(int userId, MembershipType membershipType)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new FeeCalculationException($"User {userId} not found");

        var rule = await _feeRuleRepository.GetActiveRuleAsync(user.Country, membershipType, user.Age);
        if (rule == null)
            throw new FeeCalculationException($"No active fee rule found for {user.Country}, {membershipType}, age {user.Age}");

        return rule.MonthlyFee;
    }
}