using DateSurfer.Core.Domain.Entities;

namespace DateSurfer.Core.Web.Services;

public interface ITestUserService
{
    Task<List<User>> GetTestUsersAsync();
}