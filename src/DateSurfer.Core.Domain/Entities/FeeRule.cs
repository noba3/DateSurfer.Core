using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Domain.Entities;

public class FeeRule  // ← MUSS public sein
{
    public int Id { get; set; }
    public Country Country { get; set; }
    public MembershipType MembershipType { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
    public decimal MonthlyFee { get; set; }
    public bool IsActive { get; set; } = true;
}