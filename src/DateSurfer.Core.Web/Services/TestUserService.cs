using Microsoft.EntityFrameworkCore;
using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Infrastructure.Data;

namespace DateSurfer.Core.Web.Services;

public class TestUserService : ITestUserService
{
    private readonly ApplicationDbContext _context;

    public TestUserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetTestUsersAsync()
    {
        return await _context.Users
            .OrderBy(u => u.Id)
            .Take(3)
            .ToListAsync();
    }
}