using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Domain.Enums;
using DateSurfer.Core.Domain.Interfaces;
using DateSurfer.Core.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace DateSurfer.Core.Infrastructure.Repositories;

public class FeeRuleRepository : IFeeRuleRepository
{
    private readonly ApplicationDbContext _context;

    public FeeRuleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FeeRule?> GetActiveRuleAsync(Country country, MembershipType membershipType, int age)
    {
        return await _context.FeeRules
            .Where(r => r.Country == country &&
                        r.MembershipType == membershipType &&
                        r.MinAge <= age &&
                        r.MaxAge >= age &&
                        r.IsActive)
            .FirstOrDefaultAsync();
    }
}