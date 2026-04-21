using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Domain.Entities;

public class Membership  // ← MUSS public sein
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public MembershipType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;

    public User User { get; set; } = null!;  // User ist jetzt public
}