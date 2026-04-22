using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Domain.Entities;

public class FeeRule
{
    public int Id { get; set; }
    public string Country { get; set; } = string.Empty;  // ← Jetzt string!
    public MembershipType MembershipType { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
    public decimal MonthlyFee { get; set; }
    public bool IsActive { get; set; } = true;
}