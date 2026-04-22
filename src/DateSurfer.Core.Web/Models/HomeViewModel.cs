using DateSurfer.Core.Domain.Entities;

namespace DateSurfer.Core.Web.Models;

public class HomeViewModel
{
    public List<User> TestUsers { get; set; } = new();
    public int SelectedUserId { get; set; }
    public int SelectedMembershipType { get; set; } = 1;
    public decimal? CalculatedFee { get; set; }
    public string? ErrorMessage { get; set; }
}