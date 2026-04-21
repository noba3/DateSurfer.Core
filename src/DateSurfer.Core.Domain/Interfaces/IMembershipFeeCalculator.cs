using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Domain.Interfaces;

public interface IMembershipFeeCalculator
{
    Task<decimal> CalculateFeeAsync(int userId, MembershipType membershipType);
}