using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; } = string.Empty;  // ← Jetzt string, nicht mehr enum!
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Membership? Membership { get; set; }

    public int Age => CalculateAge(DateOfBirth);

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.UtcNow;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
}