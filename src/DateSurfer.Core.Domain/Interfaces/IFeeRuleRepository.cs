using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Domain.Interfaces;

public interface IFeeRuleRepository
{
    Task<FeeRule?> GetActiveRuleAsync(Country country, MembershipType membershipType, int age);
}