using System.Threading.Tasks;


namespace AirStock.Repositories
{
    public interface IRoleAssignmentService
    {
        bool ShouldAssignUserRole(string username);
        bool ShouldAssignAdminRole(string username);

        Task<bool> AssignRoleToUserAsync(string username, string role);

        IEnumerable<string> GetRoles();

        List<string> Roles { get; }

        Task<bool> RoleExistsAsync(string roleName, string userName);
        Task<bool> CreateRoleAsync(string roleName);


    }
}
